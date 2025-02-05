using TestNodeBuilder.Lexer;

namespace TestNodeBuilder;

public partial class EditTokenForm : Form
{
    private Fsa fsa = new();

    public EditTokenForm()
    {
        InitializeComponent();
    }

    public void Init(string regex)
    {
        Invoke(() =>
        {
            regexField.Text = regex;
        });
    }

    private void saveButton_Click(object sender, EventArgs e)
    {

    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
    }

    private void EvaluateTestMatch()
    {
        if (exactCheck.Checked)
        {
            var (_, match) = fsa.Search(testInputField.Text, 0);
            matchField.Text = match ?? "";
        } else
        {
            for (int i = 0; i < testInputField.Text.Length; i++)
            {
                var (accepted, match) = fsa.Search(testInputField.Text, i);
                if (accepted == 1)
                {
                    matchField.Text = match ?? "";
                    return;
                }
            }

            matchField.Text = "";
        }
    }

    private void regexField_TextChanged(object sender, EventArgs e)
    {
        fsa = new();
        try
        {
            fsa.Build(regexField.Text, 1);

            validationLabel.Text = "Valid";
            validationLabel.ForeColor = Color.Black;

            minimizeButton.Enabled = true;
        } catch (ApplicationException ex)
        {
            fsa = new();

            validationLabel.Text = ex.Message;
            validationLabel.ForeColor = Color.Red;
        }

        EvaluateTestMatch();
    }

    private void minimizeButton_Click(object sender, EventArgs e)
    {
        fsa = fsa.ConvertToDfa().MinimizeDfa();

        minimizeButton.Enabled = false;

        EvaluateTestMatch();
    }

    private void testInputField_TextChanged(object sender, EventArgs e)
    {
        EvaluateTestMatch();
    }

    private void exactCheck_CheckedChanged(object sender, EventArgs e)
    {
        EvaluateTestMatch();
    }
}
