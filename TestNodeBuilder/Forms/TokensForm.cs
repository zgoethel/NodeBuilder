using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using TestNodeBuilder.Components;
using TestNodeBuilder.Utilities;

namespace TestNodeBuilder.Forms;

public partial class TokensForm : Form
{
    private readonly Throttle formMovedThrottle = new();

    public TokensForm(IServiceProvider sp)
    {
        InitializeComponent();

        Disposed += (_, __) =>
        {
            formMovedThrottle.Dispose();
        };

        blazorWebView1.HostPage = "wwwroot/index.html";
        blazorWebView1.Services = sp;
        blazorWebView1.RootComponents.Add<Tokens>(
            "#app",
            new Dictionary<string, object?>()
            {
                ["Form"] = this
            });

        blazorWebView1.WebView.CoreWebView2InitializationCompleted += (_, _) =>
        {
            blazorWebView1.WebView.CoreWebView2.Settings.IsPinchZoomEnabled = false;
            blazorWebView1.WebView.CoreWebView2.Settings.IsSwipeNavigationEnabled = false;
            blazorWebView1.WebView.CoreWebView2.Settings.IsZoomControlEnabled = false;
        };
    }

    private void TokensForm_Move(object sender, EventArgs e)
    {
        _ = formMovedThrottle.PerformThrottled(() =>
        {
            Invoke(() =>
            {
                // Resize to fix issue where select items don't move with window
                Width += 1;
                Width -= 1;
            });

            return Task.CompletedTask;
        }, millis: 200);
    }
}
