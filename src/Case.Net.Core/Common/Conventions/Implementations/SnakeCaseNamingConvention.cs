﻿using System;
using System.Collections.Generic;

using Case.Net.Common.Entities;
using Case.Net.Emitters.Delimiters;
using Case.Net.Emitters.Sanitizers;
using Case.Net.Emitters.Words;
using Case.Net.Extensions;
using Case.Net.Parsing;

namespace Case.Net.Common.Conventions
{
    public class SnakeCaseNamingConvention : NamingConvention
    {
        private readonly AllLowerWordEmitter        _wordEmitter;
        private readonly SingleCharDelimiterEmitter _delimiterEmitter;

        public SnakeCaseNamingConvention() : base( "snake_case" )
        {
            _wordEmitter      = new AllLowerWordEmitter( new LetterOrDigitSanitizer() );
            _delimiterEmitter = new SingleCharDelimiterEmitter( '_', false );
        }

        public override bool TryConvert(CasedString input, out CasedString output)
        {
            if ( input.IsEmpty() )
            {
                output = CasedString.Empty;

                return false;
            }

            List<string> words = new List<string>();

            for ( int i = 0; i < input.WordCount(); i++ )
            {
                if ( _wordEmitter.EmitWord( input, i, out ReadOnlySpan<char> wordBuffer ) )
                {
                    words.Add( wordBuffer.ToString() );
                }
            }

            IReadOnlyList<Delimiter> delimiters;

            if ( words.Count > 1 )
            {
                List<Delimiter> delimiterList = new List<Delimiter>();

                for ( int i = 0; i < words.Count - 1; i++ )
                {
                    if ( _delimiterEmitter.EmitDelimiter( words, i, out ReadOnlySpan<char> delimiterBuffer ) )
                    {
                        var delimiter = new Delimiter( i, delimiterBuffer.ToString() );
                        delimiterList.Add( delimiter );
                    }
                }

                delimiters = delimiterList;
            }
            else { delimiters = Array.Empty<Delimiter>(); }

            output = new CasedString( string.Empty, string.Empty, words, delimiters, this );

            return true;
        }

        public override bool TryParse(ReadOnlySpan<char> input, out CasedString output)
        {
            if ( !new SnakeCaseParser().TryParse( input, out IReadOnlyList<WordPosition> wordPositions ) )
            {
                output = CasedString.Empty;

                return false;
            }

            input.SplitWithWordPositions( wordPositions, out IReadOnlyList<string> words, out IReadOnlyList<Delimiter> delimiters );

            output = new CasedString( string.Empty, string.Empty, words, delimiters, this );

            return true;
        }

    }
}
