using System;
using System.Collections.Generic;

using Case.Net.Common.Entities;
using Case.Net.Emitters.Delimiters;
using Case.Net.Extensions;

namespace Case.Net.Common.Conventions
{
    public class RandomCaseNamingConvention : NamingConvention
    {
        private readonly SingleCharDelimiterEmitter _delimiterEmitter;
        private readonly Random                     _random;

        public RandomCaseNamingConvention() : base( "RanDOM CasE" )
        {
            _delimiterEmitter = new SingleCharDelimiterEmitter( ' ', false );
            _random           = new Random( unchecked( (int) DateTime.UtcNow.Ticks ) );
        }

        public override bool TryConvert(CasedString input, out CasedString output)
        {
            if ( input.IsEmpty() )
            {
                output = CasedString.Empty;

                return false;
            }

            IReadOnlyList<string> words    = input.Words;
            string[]              wordsOut = new string[words.Count];

            for ( int i = 0; i < words.Count; i++ )
            {
                string word   = words[i];
                char[] buffer = new char[word.Length];

                for ( int j = 0; j < word.Length; j++ )
                {
                    buffer[j] = IsUpper()
                                ? char.ToUpperInvariant( word[j] )
                                : char.ToLowerInvariant( word[j] );
                }

                wordsOut[i] = new string( buffer );
            }

            Delimiter[] delimiters = new Delimiter[words.Count - 1];

            for ( int i = 0; i < words.Count - 1; i++ )
            {
                _delimiterEmitter.EmitDelimiter( words, i, out ReadOnlySpan<char> delimiterBuffer );
                var delimiter = new Delimiter( i, delimiterBuffer.ToString() );
                delimiters[i] = delimiter;
            }

            output = new CasedString( string.Empty, string.Empty, words, delimiters, this );

            return true;
        }

        private bool IsUpper() { return _random.Next( 2 ) > 0; }

        public override bool TryParse(ReadOnlySpan<char> input, out CasedString output)
        {
            Parse( input ); // throws
            output = CasedString.Empty;

            return false;
        }

        public override CasedString Parse(ReadOnlySpan<char> input)
        {
            throw new InvalidOperationException(
                $"{nameof( RandomCaseNamingConvention )} is unable to parse"
            );
        }
    }
}
