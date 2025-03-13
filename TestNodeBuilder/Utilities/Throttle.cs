namespace TestNodeBuilder.Utilities;

public class Throttle : IDisposable
{
    private readonly SemaphoreSlim throttleMutex = new(1, 1);
    private CancellationTokenSource? throttle;

    public async Task PerformThrottled(Func<Task> work, int millis = 100)
    {
        await throttleMutex.WaitAsync();
        CancellationTokenSource cancel = new();
        try
        {
            throttle?.Cancel();
            throttle?.Dispose();
            throttle = cancel;
        } finally
        {
            throttleMutex.Release();
        }

        try
        {
            await Task.Delay(millis, cancel.Token);
        } catch (TaskCanceledException)
        {
        }

        await throttleMutex.WaitAsync();
        try
        {
            if (!cancel.IsCancellationRequested)
            {
                await work();
            }
        } finally
        {
            throttleMutex.Release();
        }
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);

        throttleMutex.Dispose();
    }
}
