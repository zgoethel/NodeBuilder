using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using System.Text.Json.Serialization;
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
        Comma,
        Exponent,
        OpenParens,
        CloseParens,
        Not
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
            fsa.Build("\\,", (int)_Token.Comma);
            fsa.Build("\\^", (int)_Token.Exponent);
            fsa.Build("\\(", (int)_Token.OpenParens);
            fsa.Build("\\)", (int)_Token.CloseParens);
            fsa.Build("\\!", (int)_Token.Not);
            fsa.Build("[ \n\r\t]+", 9999);

            fsa = fsa.ConvertToDfa().MinimizeDfa();

            var source = "((1 + 2).3.4(40, 41, 42)->5) * !!6->7 / (8^9)^10.11^(12)";
            var stream = new TokenStream(fsa, source);

            Trampoline.WorkUnit? _expr = null;
            object? expr(Trampoline.WorkBuilder a, Trampoline.WorkBuilder b)
            {
                return _expr?.Invoke(a, b);
            }

            var literal = Production.Literal(
                [
                    (int)_Token.Number
                ]);

            var parens = Production.Body(
                startToken: (int)_Token.OpenParens,
                endToken: (int)_Token.CloseParens,
                content: expr);

            var member = Production.FirstSet(
                fallback: null,
                ahead: 0,
                ((int)_Token.OpenParens, parens),
                ((int)_Token.Number, literal));

            var invokeParameterList = Production.Body(
                startToken: (int)_Token.OpenParens,
                endToken: (int)_Token.CloseParens,
                content: Production.InfixPostfixOperator(
                    [
                        ((int)_Token.Comma, true, null)
                    ],
                    expr));

            var exprA = Production.InfixPostfixOperator(
                [
                    ((int)_Token.Deref, true, null),
                    ((int)_Token.Access, true, null),
                    ((int)_Token.OpenParens, false, invokeParameterList)
                ],
                nextPrecedence: member);

            var exprB = Production.InfixPostfixOperator(
                [
                    ((int)_Token.Exponent, true, null)
                ],
                nextPrecedence: exprA,
                assoc: SD.Associativity.Right);

            var exprC = Production.PrefixOperator(
                [((int)_Token.Not, null)],
                nextPrecedence: exprB);

            var exprD = Production.InfixPostfixOperator(
                [
                    ((int)_Token.Multiply, true, null),
                    ((int)_Token.Divide, true, null)
                ],
                nextPrecedence: exprC);

            var exprE = Production.InfixPostfixOperator(
                [
                    ((int)_Token.Add, true, null),
                    ((int)_Token.Subtract, true, null)
                ],
                nextPrecedence: exprD);

            _expr = exprE;

            var parserOutput = await ParserContext.Begin(
                stream,
                async () => await Trampoline.Execute(expr, CancellationToken.None));

            //TODO Emit error if tokens remain

            // Visual Studio, you okay?
#pragma warning disable IDE0079 // Remove unnecessary suppression
#pragma warning disable CA1869 // Cache and reuse 'JsonSerializerOptions' instances
            var jsonOptions = new JsonSerializerOptions()
            {
                WriteIndented = true
            };
            jsonOptions.Converters.Add(new JsonStringEnumConverter());
#pragma warning restore CA1869 // Cache and reuse 'JsonSerializerOptions' instances
#pragma warning restore IDE0079 // Remove unnecessary suppression

            var json = JsonSerializer.Serialize(parserOutput, jsonOptions);
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