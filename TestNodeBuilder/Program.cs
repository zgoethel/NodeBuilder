using Microsoft.Extensions.DependencyInjection;

namespace TestNodeBuilder;

internal static class Program
{
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        var services = new ServiceCollection();

        services.AddWindowsFormsBlazorWebView();
        services.AddSingleton<WindowRegistry>();
        services.AddTransient<Form1>();
        services.AddTransient<Form2>();
        services.AddTransient<Form3>();

        var sp = services.BuildServiceProvider();

        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        Application.Run(sp.GetRequiredService<Form1>());
    }
}