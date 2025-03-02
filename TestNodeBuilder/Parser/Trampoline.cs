namespace TestNodeBuilder.Parser;

public static class Trampoline
{
    public delegate object? WorkUnit(Func<WorkUnit, WorkUnitResult> addWork, Func<WorkUnit, WorkUnitResult> addTail);

    public class WorkUnitResult(WorkUnit work)
    {
        public WorkUnit Work { get; } = work;

        public object? Result { get; set; }
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

        WorkUnitResult addTail(WorkUnit w)
        {
            var result = new WorkUnitResult(w);
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
                    doWork.Result = await Task.Run(() => doWork.Work(addWork, addTail), cancel);

                    continue;
                }

                if (tails.Count > 0)
                {
                    var doWork = tails.Pop();
                    doWork.Result = await Task.Run(() => doWork.Work(addWork, addTail), cancel);

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
