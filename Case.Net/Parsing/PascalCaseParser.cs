namespace Case.Net.Parsing;

public class PascalCaseParser : IWordParser
{

    public bool TryGetNextWord(
        ReadOnlySpan<char> inputSlice,
        out ReadOnlySpan<char> word,
        out ReadOnlySpan<char> delimiter
    )
    {
        word      = ReadOnlySpan<char>.Empty;
        delimiter = ReadOnlySpan<char>.Empty;

        for ( int i = 0; i < inputSlice.Length - 1; i++ )
        {
            char current = inputSlice[i];

            if ( !char.IsLetterOrDigit( current ) ) { return false; }

            char next = inputSlice[i + 1];

            if ( !char.IsUpper( next ) ) { continue; }

            word = inputSlice[..(i + 1)];

            return true;
        }

        word = inputSlice;

        return true;
    }
}
