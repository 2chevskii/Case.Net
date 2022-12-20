namespace Case.Net.Parsing;

public interface IWordParser
{
    bool TryParse(ReadOnlySpan<char> input, out IReadOnlyList<WordPosition> words);
}
