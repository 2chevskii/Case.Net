namespace Case.Net.Parsing.Suffixes;

public interface ISuffixParser
{
    int GetSuffixSize(ReadOnlySpan<char> input);
}
