namespace Case.Net.Parsing;

public class KebabCaseParser : IWordParser
{
    public bool TryGetNextWord(
        ReadOnlySpan<char> inputSlice,
        out ReadOnlySpan<char> word,
        out ReadOnlySpan<char> delimiter
    )
    {
        word = delimiter = ReadOnlySpan<char>.Empty;

        for ( int i = 0; i < inputSlice.Length; i++ )
        {
            var current = inputSlice[i];

            if ( !char.IsLetterOrDigit( current ) ) { return false; }

            if ( i == inputSlice.Length - 1 ) { break; }

            var next = inputSlice[i + 1];

            if ( next != '-' ) { continue; }

            word      = inputSlice[..(i + 1)];
            delimiter = inputSlice.Slice( i + 1, 1 );
        }

        word = inputSlice;

        return true;
    }
}
