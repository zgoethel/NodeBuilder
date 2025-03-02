using TestNodeBuilder.Lexer;

namespace TestNodeBuilder.Parser;

public static class ParserContext
{
    private static readonly AsyncLocal<TokenStream> tokenStream = new();
    public static TokenStream TokenStream => tokenStream.Value!;

    public static async Task<T> Begin<T>(TokenStream tokenStream, Func<Task<T>> work)
    {
        ParserContext.tokenStream.Value = tokenStream;

        return await work();
    }
}
