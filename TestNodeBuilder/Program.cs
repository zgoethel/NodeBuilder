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
        Add,
        Subtract,
        Multiply,
        Divide,
        Deref,
        Access,
        Invoke,
        Exponent,
        OpenParens,
        CloseParens
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
            fsa.Build("\\-\\>", (int)_Token.Deref);
            fsa.Build("\\-", (int)_Token.Subtract);
            fsa.Build("\\*", (int)_Token.Multiply);
            fsa.Build("\\/", (int)_Token.Divide);
            fsa.Build("\\.", (int)_Token.Access);
            fsa.Build("\\(\\)", (int)_Token.Invoke); //TEMP
            fsa.Build("\\^", (int)_Token.Exponent);
            fsa.Build("\\(", (int)_Token.OpenParens);
            fsa.Build("\\)", (int)_Token.CloseParens);
            fsa.Build("[ \n\r\t]+", 9999);

            fsa = fsa.ConvertToDfa().MinimizeDfa();

            var source = "((1 + 2).3.4()->5) * 6->7 / (8^9)^10";
            var stream = new TokenStream(fsa, source);

            Trampoline.WorkUnit? expr = null;

            var literal = Production.Literal(
                [(int)_Token.Number]);
            var parens = Production.Body(
                (int)_Token.OpenParens,
                (int)_Token.CloseParens,
                (a, b) => expr!.Invoke(a, b));
            var member = Production.FirstSet(
                null,
                ((int)_Token.OpenParens, parens),
                ((int)_Token.Number, literal));

            var exprA = Production.InfixPostfixOperator(
                [
                    ((int)_Token.Deref, true),
                    ((int)_Token.Access, true),
                    ((int)_Token.Invoke, false)
                ],
                member);
            var exprB = Production.InfixPostfixOperator(
                [
                    ((int)_Token.Exponent, true)
                ],
                exprA,
                assoc: SD.Associativity.Right);
            var exprC = Production.InfixPostfixOperator(
                [
                    ((int)_Token.Multiply, true),
                    ((int)_Token.Divide, true)
                ],
                exprB);
            var exprD = Production.InfixPostfixOperator(
                [
                    ((int)_Token.Add, true),
                    ((int)_Token.Subtract, true)
                ],
                exprC);

            expr = exprD;

            var parserOutput = await ParserContext.Begin(
                stream,
                async () => await Trampoline.Execute(expr!, CancellationToken.None));

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