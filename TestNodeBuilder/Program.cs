using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using TestNodeBuilder.Lexer;
using TestNodeBuilder.Parser;

namespace TestNodeBuilder;

internal static class Program
{
    enum _Token
    {
        Number = 1,
        Add = 2,
        Subtract = 3,
        Multiply = 4,
        Divide = 5
    }

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        _ = Task.Run(async () =>
        {
            var fsa = new Fsa();
            fsa.Build("[0-9]+", (int)_Token.Number);
            fsa.Build("\\+", (int)_Token.Add);
            fsa.Build("\\-", (int)_Token.Subtract);
            fsa.Build("\\*", (int)_Token.Multiply);
            fsa.Build("\\/", (int)_Token.Divide);
            fsa.Build("[ \n\r\t]+", 9999);

            fsa = fsa.ConvertToDfa().MinimizeDfa();

            var source = "1 + 2 * 3 / 4 - 5 - 6 * 7 * 8";
            var stream = new TokenStream(fsa, source);

            var literal = Production.Literal([(int)_Token.Number]);
            var exprA = Production.BinaryOperator([(int)_Token.Multiply, (int)_Token.Divide], literal);
            var exprB = Production.BinaryOperator([(int)_Token.Add, (int)_Token.Subtract], exprA);
            var expr = exprB;

            var parserOutput = await ParserContext.Begin(
                stream,
                async () =>  await Trampoline.Execute(expr, CancellationToken.None));

            //TODO Emit error if tokens remain

            var json = JsonSerializer.Serialize(
                parserOutput ?? "",
                options: new()
                {
                    WriteIndented = true
                });
        }); // Breakpoint here

        var services = new ServiceCollection();

        services.AddWindowsFormsBlazorWebView();
        services.AddTransient<HomeForm>();
        services.AddTransient<EditTokenForm>();

        var sp = services.BuildServiceProvider();

        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        Application.Run(sp.GetRequiredService<HomeForm>());
    }
}