namespace Case.Net.Parsing.Prefixes;

public class GenericPrefixParser : IPrefixParser
{
    public IReadOnlyList<char> PrefixChars { get; }

    public GenericPrefixParser(params char[] prefixChars) { PrefixChars = prefixChars; }

    public virtual int GetPrefixSize(ReadOnlySpan<char> input)
    {
        if ( input.Length < 2 )
            return 0;

        int i = 0;

        for ( ; i < input.Length - 1; i++ )
        {
            char current = input[i];

            if ( !PrefixChars.Contains( current ) )
            {
                break;
            }
        }

        return i;
    }
}
