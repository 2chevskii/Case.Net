namespace Case.Net.Parsing;

public interface IWordParser
{
    bool TryGetNextWord(
        ReadOnlySpan<char> inputSlice,
        out ReadOnlySpan<char> word,
        out ReadOnlySpan<char> delimiter
    );
}
