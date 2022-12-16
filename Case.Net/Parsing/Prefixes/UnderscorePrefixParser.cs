namespace Case.Net.Parsing.Prefixes;

public sealed class UnderscorePrefixParser : IPrefixParser
{
    public int GetPrefixSize(ReadOnlySpan<char> input)
    {
        if ( input.Length < 2 )
            return 0;

        if ( input[0] is not '_' )
            return 0;

        return 1;
    }
}
