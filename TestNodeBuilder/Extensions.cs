namespace TestNodeBuilder;

public static class Extensions
{
    public static async Task<DialogResult> ShowDialogAsync(this Form form)
    {
        TaskCompletionSource<DialogResult> task = new();

        SynchronizationContext.Current!.Post(
            (_) =>
            {
                var dialogResult = form.ShowDialog();
                task.SetResult(dialogResult);
            },
            null);

        return await task.Task;
    }
}
