using System;
using System.Collections.Generic;

using Case.Net.Extensions;

namespace Case.Net.Parsing
{
    public class DelimiterLowerCaseParser : IWordParser
    {
        public readonly char Delimiter;

        public DelimiterLowerCaseParser(char delimiter) { Delimiter = delimiter; }

        public bool TryParse(ReadOnlySpan<char> input, out IReadOnlyList<WordPosition> words)
        {
            if ( input.IsEmpty )
            {
                words = Array.Empty<WordPosition>();

                return false;
            }

            List<WordPosition> wordsRw = new List<WordPosition>();
            words = wordsRw;

            for ( int i = 0; i < input.Length; i++ )
            {
                char current = input[i];

                if ( !current.IsLower() && !current.IsDigit() )
                {
                    return false;
                }

                if ( input.IsAtEnd( i ) )
                {
                    wordsRw.Add( new WordPosition( i, i ) );

                    break;
                }

                if ( IsDelimiter( input, i + 1 ) )
                {
                    wordsRw.Add( new WordPosition( i, i + 1 ) );
                    i++;
                }
            }

            return true;
        }

        private bool IsDelimiter(ReadOnlySpan<char> input, int index)
        {
            if ( index >= input.Length )
            {
                return false;
            }

            return input[index] == Delimiter;
        }
    }
}
