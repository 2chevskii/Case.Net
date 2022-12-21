using System;
using System.Collections.Generic;
using System.Xml.Schema;

using Case.Net.Common;
using Case.Net.Common.Entities;
using Case.Net.Parsing;

namespace Case.Net.Extensions
{
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
            out IReadOnlyList<Delimiter> delimiters
        )
        {
            List<string>    wordsRw      = new List<string>();
            List<Delimiter> delimitersRw = new List<Delimiter>();
            words      = wordsRw;
            delimiters = delimitersRw;

            int wordStart = 0;

            for ( int i = 0; i < wordPositions.Count; i++ )
            {
                (int wordEnd, int delimiterEnd) = wordPositions[i];

                int                wordLength = wordEnd - wordStart + 1;
                ReadOnlySpan<char> wordSlice  = input.Slice( wordStart, wordLength );
                wordsRw.Add( wordSlice.ToString() );

                if ( wordPositions[i].HasDelimiter )
                {
                    int delimiterStart  = wordEnd + 1;
                    int delimiterLength = delimiterEnd - delimiterStart + 1;

                    ReadOnlySpan<char> delimiterSlice = input.Slice(
                        delimiterStart,
                        delimiterLength
                    );

                    delimitersRw.Add( new Delimiter( i, delimiterSlice.ToString() ) );
                    wordStart = delimiterEnd + 1;
                }
                else { wordStart = wordEnd + 1; }
            }
        }

        public static ReadOnlySpan<char> ToLowerInvariant(this ReadOnlySpan<char> self)
        {
#if NET5_0_OR_GREATER
            Span<char> target =
 new Span<char>( GC.AllocateUninitializedArray<char>( self.Length ) );
#else
            Span<char> target = new Span<char>( new char[self.Length] );
#endif

            self.ToLowerInvariant( target );

            return target;
        }

        public static ReadOnlySpan<char> ToUpperInvariant(this ReadOnlySpan<char> self)
        {
#if NET5_0_OR_GREATER
            Span<char> target =
 new Span<char>( GC.AllocateUninitializedArray<char>( self.Length ) );
#else
            Span<char> target = new Span<char>( new char[self.Length] );
#endif

            self.ToUpperInvariant( target );

            return self;
        }
    }
}
