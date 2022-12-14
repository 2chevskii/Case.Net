namespace Case.Net.Parsing.Splitters;

public class CamelCaseSplitter : ISplitter
{
    public bool TryFindSplitIndex(ReadOnlySpan<char> input, ref int wordEnd, ref int nextPosition)
    {
        for ( int i = 0; i < input.Length - 1; i++ )
        {
            char current = input[i];

            if ( !char.IsLower( current ) )
                continue;

            int j = i + 1;

            char next = input[j];

            if ( !char.IsUpper( next ) )
                continue;

            wordEnd      = i;
            nextPosition = j;
            return true;
        }

        return false;
    }
}
