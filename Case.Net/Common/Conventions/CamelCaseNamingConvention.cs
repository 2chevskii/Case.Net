﻿using Case.Net.Emit.Words;
using Case.Net.Extensions;
using Case.Net.Parsing;

namespace Case.Net.Common.Conventions;

public class CamelCaseNamingConvention : INamingConvention
{
    private readonly IWordEmitter _wordEmitter = CamelCaseWordEmitter.Instance;

    public string Name => "camelCase";

    public CasedString Convert(CasedString source)
    {
        List<string> words = new ();

        for ( int i = 0; i < source.WordCount(); i++ )
        {
            var word = _wordEmitter.EmitWord( source, i );
            words.Add( word );
        }

        // return new CasedString( string.Empty, string.Empty, Array.Empty<string>(), words, this );
        throw new NotImplementedException();

    }

    public CasedString Parse(ReadOnlySpan<char> input)
    {
        if ( !TryParse( input, out CasedString output ) )
        {
            throw new Exception( "Failed to parse input" );
        }

        return output;
    }

    public bool TryParse(ReadOnlySpan<char> input, out CasedString output)
    {
        output = CasedString.Empty;

        var parser = new CamelCaseParser();

        var position  = 0;
        var words     = new List<Word>();
        var wordIndex = 0;

        while ( position < input.Length )
        {
            if ( !parser.TryGetNextWord(
                     input.Slice( position ),
                     out var wordSlice,
                     out var delimiterSlice
                 ) )
            {
                return false;
            }


        }

        throw new NotImplementedException();

        /*if ( input.Length is 0 ) { return false; }

        /*camelCase string should start with a lower letter#1#
        if ( !char.IsLower( input[0] ) ) { return false; }

        if ( input.Length is 1 )
        {
            output = new CasedString( new[] {input.ToString()}, this );

            return true;
        }

        List<string> words             = new ();
        int          wordStartPosition = 0;

        for ( int i = 0; i < input.Length; i++ )
        {
            char current = input[i];

            if ( !char.IsLetterOrDigit( current ) )
            {
                return false;
            }

            if ( i == input.Length - 1 )
            {
                words.Add( input.Slice( wordStartPosition ).ToString() );

                break;
            }

            char next = input[i + 1];

            bool isWordEnd   = char.IsLower( current ) || char.IsDigit( current );
            bool isWordStart = char.IsUpper( next );

            if ( isWordEnd && isWordStart )
            {
                var wordLength = i - wordStartPosition + 1;
                words.Add(input.Slice(wordStartPosition, wordLength).ToString());
                wordStartPosition += wordLength;
            }
        }

        output = new CasedString( words, this );

        return true;*/
    }

}
