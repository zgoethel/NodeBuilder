namespace TestNodeBuilder;

internal partial class Form3 : Form
{
    public Form3(IServiceProvider sp)
    {
        InitializeComponent();

        button1.Click += (_, __) =>
        {
            webView21.GoBack();
        };

        button2.Click += (_, __) =>
        {
            webView21.Reload();
        };

        button3.Click += (_, __) =>
        {
            if (!textBox1.Text.StartsWith("http://", StringComparison.CurrentCultureIgnoreCase)
                && !textBox1.Text.StartsWith("https://", StringComparison.CurrentCultureIgnoreCase))
            {
                textBox1.Text = "http://" + textBox1.Text;
            }

            try
            {
                webView21.Source = new(textBox1.Text);
            } catch (Exception ex)
            {
                SynchronizationContext.Current!.Post(
                    (_) =>
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    },
                    null);
            }
        };

        webView21.SourceChanged += (_, __) =>
        {
            textBox1.Text = webView21.Source.ToString();
            button1.Enabled = webView21.CanGoBack;
        };

        webView21.Source = new("https://www.spacejam.com/1996/");
    }
}
