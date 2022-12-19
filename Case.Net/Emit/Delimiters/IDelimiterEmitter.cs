namespace Case.Net.Emit.Delimiters;

public interface IDelimiterEmitter
{
    string EmitDelimiter(IReadOnlyList<string> words, int index);
}
