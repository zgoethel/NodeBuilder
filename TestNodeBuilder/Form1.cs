using Microsoft.AspNetCore.Components.WebView.WindowsForms;

namespace TestNodeBuilder;

internal partial class Form1 : Form
{
    private int index;

    public Form1(IServiceProvider sp, WindowRegistry windowRegistry)
    {
        InitializeComponent();

        index = windowRegistry.AllForms.Count;
        windowRegistry.AllForms.Add(this);

        blazorWebView1.HostPage = "wwwroot/index.html";
        blazorWebView1.Services = sp;
        blazorWebView1.RootComponents.Add<Router>(
            "#app",
            new Dictionary<string, object?>()
            {
                ["Index"] = index
            });
    }
}
