namespace Case.Net.Parsing.Suffixes;

public class GenericSuffixParser : ISuffixParser
{
    public IReadOnlyList<char> SuffixChars { get; }

    public GenericSuffixParser(params char[] suffixChars) { SuffixChars = suffixChars; }

    public virtual int GetSuffixSize(ReadOnlySpan<char> input)
    {
        if ( input.Length < 2 )
            return 0;

        int i = input.Length - 1;

        for ( ; i >= 0; i-- )
        {
            char current = input[i];

            if ( !SuffixChars.Contains( current ) )
                break;
        }

        return input.Length - 1 - i;
    }
}
