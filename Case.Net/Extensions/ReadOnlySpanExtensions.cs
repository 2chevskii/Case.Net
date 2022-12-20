using System.Xml.Schema;

using Case.Net.Parsing;

namespace Case.Net.Extensions;

public static class ReadOnlySpanExtensions
{
    public static bool IsAtEnd<T>(this ReadOnlySpan<T> self, int index)
    {
        return self.Length - 1 == index;
    }

    public static void SplitWithWordPositions(
        this ReadOnlySpan<char> input,
        IReadOnlyList<WordPosition> wordPositions,
        out IReadOnlyList<string> words,
        out IReadOnlyList<string> delimiters
    )
    {
        var wordsRw      = new List<string>();
        var delimitersRw = new List<string>();
        words      = wordsRw;
        delimiters = delimitersRw;

        int wordStart = 0;

        for ( int i = 0; i < wordPositions.Count; i++ )
        {
            var (wordEnd, delimiterEnd) = wordPositions[i];

            var wordLength = wordEnd - wordStart + 1;
            var wordSlice  = input.Slice( wordStart, wordLength );
            wordsRw.Add( wordSlice.ToString() );

            if ( wordPositions[i].HasDelimiter )
            {
                var delimiterStart  = wordEnd + 1;
                var delimiterLength = delimiterEnd - delimiterStart + 1;

                var delimiterSlice = input.Slice( delimiterStart, delimiterLength );
                delimitersRw.Add( delimiterSlice.ToString() );
                wordStart = delimiterEnd + 1;
            }
            else { wordStart = wordEnd + 1; }
        }
    }

    public static ReadOnlySpan<char> ToLowerInvariant(this ReadOnlySpan<char> self)
    {
        Span<char> target = new Span<char>( GC.AllocateUninitializedArray<char>( self.Length ) );

        /*for ( int i = 0; i < self.Length; i++ ) { target[i] = char.ToLowerInvariant( self[i] ); }

        return target;*/

        self.ToLowerInvariant( target );

        return target;
    }

    public static ReadOnlySpan<char> ToUpperInvariant(this ReadOnlySpan<char> self)
    {
        Span<char> target = new Span<char>( GC.AllocateUninitializedArray<char>( self.Length ) );

        /*for ( int i = 0; i < self.Length; i++ ) { target[i] = char.ToUpperInvariant( self[i] ); }

        return target;*/

        self.ToUpperInvariant( target );

        return self;
    }
}
