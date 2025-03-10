using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using TestNodeBuilder.Components;

namespace TestNodeBuilder;

public partial class HomeForm : Form
{
    public HomeForm(IServiceProvider sp)
    {
        InitializeComponent();

        blazorWebView1.HostPage = "wwwroot/index.html";
        blazorWebView1.Services = sp;
        blazorWebView1.RootComponents.Add<Home>(
            "#app",
            new Dictionary<string, object?>()
            {
                ["Form"] = this
            });

        blazorWebView1.WebView.CoreWebView2InitializationCompleted += (_, _) =>
        {
            blazorWebView1.WebView.CoreWebView2.Settings.IsPinchZoomEnabled = false;
            blazorWebView1.WebView.CoreWebView2.Settings.IsSwipeNavigationEnabled = false;
        };
    }
}
