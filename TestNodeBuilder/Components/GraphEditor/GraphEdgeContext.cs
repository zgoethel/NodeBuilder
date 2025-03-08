using Microsoft.JSInterop;

namespace TestNodeBuilder.Components.GraphEditor;

public class GraphEdgeContext(IJSRuntime js)
{
    public class DomEdge
    {
        public string SelectorFrom { get; set; } = "";

        public string SelectorTo { get; set; } = "";
    }

    public class PlottedEdge(DomEdge createdFrom)
    {
        public (double x, double y) From { get; set; }

        public (double x, double y) To { get; set; }

        public DomEdge CreatedFrom { get; } = createdFrom;
    }

    public List<DomEdge> DomEdges { get; } = [];

    public List<PlottedEdge> PlottedEdges { get; private set; } = [];

    public event Func<Task>? EdgesChanged;

    private IJSObjectReference? domUtil;

    public async Task InvokeEdgesChanged()
    {
        foreach (var f in EdgesChanged?.GetInvocationList()?.Cast<Func<Task>>() ?? [])
        {
            await f();
        }
    }

    public async Task RebuildPlottedEdges()
    {
        domUtil ??= await js.InvokeAsync<IJSObjectReference>("import", "./js/DomUtil.js");

        var newEdges = new List<PlottedEdge>();

        foreach (var edge in DomEdges)
        {
            var _from = await domUtil.InvokeAsync<string>("getCenterCoords", edge.SelectorFrom, ".graph-pane .origin");
            var from = _from.Split(",").Select(double.Parse).ToArray();

            var _to = await domUtil.InvokeAsync<string>("getCenterCoords", edge.SelectorTo, ".graph-pane .origin");
            var to = _to.Split(",").Select(double.Parse).ToArray();

            newEdges.Add(new(edge)
            {
                To = (to[0], to[1]),
                From = (from[0], from[1])
            });
        }

        PlottedEdges = newEdges;

        await InvokeEdgesChanged();
    }
}
