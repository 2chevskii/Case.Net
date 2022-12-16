namespace Case.Net.Parsing.Splitters;

public class CamelCaseSplitter : ISplitter
{
    public SplitResult Split(ReadOnlySpan<char> input)
    {
        for ( int i = 0; i < input.Length - 1; i++ )
        {
            char current = input[i];
            char next    = input[i + 1];

            if ( !char.IsLower( current ) )
            {
                continue;
            }

            if ( !char.IsUpper( next ) )
            {
                continue;
            }

            return new SplitResult( i + 1 );
        }

        return SplitResult.CreateEmpty();
    }
}
