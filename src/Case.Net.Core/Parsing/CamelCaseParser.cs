using System;
using System.Collections.Generic;

using Case.Net.Extensions;

namespace Case.Net.Parsing
{
    public class CamelCaseParser : IWordParser
    {
        public bool TryParse(ReadOnlySpan<char> input, out IReadOnlyList<WordPosition> words)
        {
            /*we cannot parse an empty string*/
            if ( input.IsEmpty )
            {
                words = Array.Empty<WordPosition>();

                return false;
            }

            /*camel case does not allow first char to be anything but lowercase letter*/
            if ( !input[0].IsLower() )
            {
                words = Array.Empty<WordPosition>();

                return false;
            }

            List<WordPosition> wordsRw = new List<WordPosition>();
            words = wordsRw;

            for ( int i = 0; i < input.Length; i++ )
            {
                char current = input[i];

                /*camel case does only allow letters or digits*/
                if ( !current.IsLetterOrDigit() ) { return false; }

                if ( IsWordStart( input, i + 1 ) || input.IsAtEnd( i ) )
                {
                    wordsRw.Add( new WordPosition( i, i ) );
                }
            }

            return true;
        }

        public static bool IsWordStart(ReadOnlySpan<char> input, int index)
        {
            if ( index >= input.Length )
            {
                return false;
            }

            return input[index].IsUpper();
        }
    }
}
