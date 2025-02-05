namespace TestNodeBuilder;

internal class WindowRegistry
{
    public List<Form1> AllForms { get; set; } = [];

    private int formIndex = 0;
    public int FormIndex
    {
        get => formIndex;
        set
        {
            formIndex = value;
            FormIndexChanged(value);
        }
    }

    public event Action<int> FormIndexChanged = (_) => { };
}
