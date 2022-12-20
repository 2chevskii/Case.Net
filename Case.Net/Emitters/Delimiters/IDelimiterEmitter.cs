namespace Case.Net.Emit.Delimiters;

public interface IDelimiterEmitter
{
    bool EmitDelimiter(IReadOnlyList<string> words, int index, out ReadOnlySpan<char> delimiterBuffer);
}
