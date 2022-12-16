namespace Case.Net.Parsing.Prefixes;

public interface IPrefixParser
{
    int GetPrefixSize(ReadOnlySpan<char> input);
}
