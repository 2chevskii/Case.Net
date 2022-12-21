using System;
using System.Collections.Generic;

using Case.Net.Extensions;

namespace Case.Net.Parsing
{
    public class MixedParser : IWordParser
    {

        public bool TryParse(ReadOnlySpan<char> input, out IReadOnlyList<WordPosition> words)
        {
            if ( input.IsEmpty )
            {
                words = Array.Empty<WordPosition>();

                return true;
            }

            var wordsRw = new List<WordPosition>();
            words = wordsRw;

            int wordStart = 0;
            int pos       = 0;

            for ( ; pos < input.Length; pos++ )
            {
                if ( input.Length - 1 == pos )
                {
                    // finish parsing
                }

                char current = input[pos];

                if ( input.Length - 2 > pos )
                {
                    char next = input[pos + 1];

                    if ( current.IsLower() && next.IsUpper() )
                    {

                    }

                    if ( current.IsLetter() && next.IsDigit() )
                    {

                    }
                }


            }
        }
    }
}
