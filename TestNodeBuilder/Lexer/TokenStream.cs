namespace TestNodeBuilder.Lexer;

/// <summary>
/// Encapsulates a source file and also the current cursor state of the lexer-
/// parser.It supports a single token of lookahead, which allows polling a
/// "current" token into memory then peeking the "next" token off the top of the
/// token stream.
/// </summary>
public class TokenStream(Fsa grammar, string source)
{
    public Fsa Grammar { get; } = grammar;
    public string Source { get; } = source;

    /// <summary>
    /// Cursor position can be reset, but take care to anchor to token starts, to
    /// remain within source, and to clear the cached "next token."
    /// </summary>
    public int Offset { get; private set; }

    /// <summary>
    /// Seeks the source cursor to the specified offset, clearing the cached
    /// token to ensure it gets re-evaluated.
    /// </summary>
    /// <param name="offset"></param>
    public void Seek(int offset)
    {
        Offset = offset;
        nextToken = null;
    }

    /// <summary>
    /// Allows inspection of the current source position in the debugger.
    /// </summary>
    public string Remaining => Source[Offset..];

    // Cached "next token" value re-evaluated when peeking
    private int? nextToken;
    public int Next
    {
        get
        {
            if (nextToken is not null)
            {
                return nextToken.Value;
            }
            return Peek();
        }
    }

    /// <summary>
    /// String content matched by the previous token, which can be further
    /// interpreted for literals or identifiers.
    /// </summary>
    public string Text { get; private set; } = "";

    /// <summary>
    /// Searches the grammar for a token matching the characters found at the
    /// source's cursor offset and onwards. Whitespace will be ignored.
    /// </summary>
    public int Peek()
    {
        if (Offset >= Source.Length)
        {
            Text = "";
            return (nextToken = -1).Value;
        }

        var (accepted, match) = Grammar.Search(Source, Offset);
        Text = match;
        nextToken = accepted;

        // Discard whitespace/comments
        if (accepted == 9999)
        {
            Poll();
            return Peek();
        }

        return accepted;
    }

    /// <summary>
    /// Searches the grammar for a token matching the characters found at the
    /// source's cursor offset and onwards. Whitespace will be ignored. After
    /// calling, the matched length of text will be consumed from source (and the
    /// cursor will move forward by that number of characters).
    /// </summary>
    public int Poll()
    {
        var token = Next;
        nextToken = null;

        Offset += Text.Length;

        return token;
    }
}
