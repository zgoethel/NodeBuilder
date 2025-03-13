using TestNodeBuilder.Lexer;
using TestNodeBuilder.Utilities;

namespace TestNodeBuilder.Forms;

public partial class EditTokenForm : Form
{
    private string initialRegex = "";
    private Fsa fsa = new();
    private Func<string, string>? validate;

    public EditTokenForm()
    {
        InitializeComponent();
    }

    public async Task<string?> ShowEditTokenForm(string regex, Func<string, string> validate)
    {
        this.validate = validate;
        initialRegex = regex;

        var result = await this.ShowDialogAsync();
        if (result != DialogResult.OK)
        {
            return null;
        }

        return regexField.Text;
    }

    private void EditTokenForm_Load(object sender, EventArgs e)
    {
        regexField.Text = initialRegex;
    }

    private void regexField_TextChanged(object sender, EventArgs e)
    {
        CompileRegex();
    }

    private void minimizeButton_Click(object sender, EventArgs e)
    {
        fsa = fsa.ConvertToDfa().MinimizeDfa();

        MessageBox.Show($"Resulting DFA has {fsa.Flat.Count} states.", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

        minimizeButton.Enabled = false;
        testInputField.Focus();

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

    private void saveButton_Click(object sender, EventArgs e)
    {
        if (validationLabel.Text != "Valid")
        {
            MessageBox.Show(validationLabel.Text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return;
        }

        var error = validate?.Invoke(regexField.Text);
        if (string.IsNullOrEmpty(error))
        {
            DialogResult = DialogResult.OK;
            Close();
        } else
        {
            MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
    }

    private void CompileRegex()
    {
        try
        {
            minimizeButton.Enabled = false;

            fsa = new();
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
}
