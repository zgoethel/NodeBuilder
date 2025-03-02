namespace TestNodeBuilder.Parser;

public static class Trampoline
{
    public delegate WorkUnitResult WorkBuilder(WorkUnit work);
    public delegate object? WorkUnit(WorkBuilder addWork, WorkBuilder addTail);

    public class WorkUnitResult(WorkUnit work, WorkUnitResult? parent = null)
    {
        public WorkUnit Work { get; } = work;

        public object? Result { get; set; }

        public WorkUnitResult? Parent { get; set; } = parent;
    }

    public static async Task<object?> Execute(WorkUnit initialWork, CancellationToken cancel)
    {
        var work = new Queue<WorkUnitResult>();
        var tails = new Stack<WorkUnitResult>();

        WorkUnitResult addWork(WorkUnit w)
        {
            var result = new WorkUnitResult(w);
            work.Enqueue(result);

            return result;
        }

        WorkUnitResult addTail(WorkUnit w, WorkUnitResult? parent = null)
        {
            var result = new WorkUnitResult(w, parent: parent);
            tails.Push(result);

            return result;
        }

        try
        {
            var result = new WorkUnitResult(initialWork);
            work.Enqueue(result);

            while (work.Count > 0 || tails.Count > 0)
            {
                if (work.Count > 0)
                {
                    var doWork = work.Dequeue();
                    doWork.Result = await Task.Run(
                        () => doWork.Work(addWork, (it) => addTail(it, parent: doWork)),
                        cancel);

                    continue;
                }

                if (tails.Count > 0)
                {
                    var doWork = tails.Pop();
                    doWork.Result = await Task.Run(
                        () => doWork.Work(addWork, (it) => addTail(it, parent: doWork)),
                        cancel);

                    for (var p = doWork.Parent; p is not null; p = p.Parent)
                    {
                        p.Result = doWork.Result;
                    }

                    continue;
                }
            }

            return result.Result;
        } catch (Exception)
        {
            work.Clear();
            tails.Clear();

            throw;
        }
    }
}
