using System.Threading;

namespace TestNodeBuilder;

public static class Extensions
{
    public static void ShowDialogSynchronized(this Form form)
    {
        SynchronizationContext.Current!.Post((_) => form.ShowDialog(), null);
    }
}
