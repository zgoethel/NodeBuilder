using Microsoft.JSInterop;

namespace TestNodeBuilder.Components.GraphEditor;

public class GraphEdgeContext(IJSRuntime js) : IDisposable
{
    public class DomEdge
    {
        public string SelectorFrom { get; set; } = "";

        public string SelectorTo { get; set; } = "";

        public decimal? Width { get; set; }

        public string? Color { get; set; }

        public bool? Dashed { get; set; }
    }

    public class PlottedEdge(DomEdge createdFrom)
    {
        public (double x, double y) From { get; set; }

        public (double x, double y) To { get; set; }

        public DomEdge CreatedFrom { get; } = createdFrom;
    }

    public string OriginSelector { get; set; } = ".graph-origin";

    public List<DomEdge> DomEdges { get; } = [];

    public List<PlottedEdge> PlottedEdges { get; private set; } = [];

    public event Func<Task>? EdgesChanged;

    private IJSObjectReference? domUtil;
    private readonly SemaphoreSlim mutexLock = new(1, 1);
    private readonly SemaphoreSlim queueLock = new(1, 1);

    public async Task InvokeEdgesChanged()
    {
        foreach (var f in EdgesChanged?.GetInvocationList()?.Cast<Func<Task>>() ?? [])
        {
            await f();
        }
    }

    private async Task _Throttle(Func<Task> work)
    {
        if (!await queueLock.WaitAsync(0))
        {
            return;
        }
        try
        {
            await mutexLock.WaitAsync();
        } finally
        {
            queueLock.Release();
        }
        try
        {
            await work();
        } finally
        {
            mutexLock.Release();
        }
    }

    public async Task RebuildPlottedEdges(double scale = 1.0)
    {
        await _Throttle(async () =>
        {
            domUtil ??= await js.InvokeAsync<IJSObjectReference>("import", "./js/DomUtil.js");

            var newEdges = new List<PlottedEdge>();

            foreach (var edge in DomEdges)
            {
                var _from = await domUtil.InvokeAsync<string>("getCenterCoords", edge.SelectorFrom, OriginSelector);
                if (string.IsNullOrEmpty(_from))
                {
                    continue;
                }
                var from = _from.Split(",").Select(double.Parse).ToArray();

                var _to = await domUtil.InvokeAsync<string>("getCenterCoords", edge.SelectorTo, OriginSelector);
                if (string.IsNullOrEmpty(_to))
                {
                    continue;
                }
                var to = _to.Split(",").Select(double.Parse).ToArray();

                newEdges.Add(new(edge)
                {
                    To = (to[0] / scale, to[1] / scale),
                    From = (from[0] / scale, from[1] / scale)
                });
            }

            PlottedEdges = newEdges;

            await InvokeEdgesChanged();
        });
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);

        mutexLock.Dispose();
        queueLock.Dispose();
    }
}
