namespace Case.Net.Emit.Delimiters;

public class NoDelimiterEmitter : IDelimiterEmitter
{

    public bool EmitDelimiter(
        IReadOnlyList<string> words,
        int index,
        out ReadOnlySpan<char> delimiterBuffer
    )
    {
        delimiterBuffer=ReadOnlySpan<char>.Empty;
        return false;
    }
}
