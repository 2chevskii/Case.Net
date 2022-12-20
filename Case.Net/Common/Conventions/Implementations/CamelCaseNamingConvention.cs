using Case.Net.Common.Entities;
using Case.Net.Emitters.Words;
using Case.Net.Extensions;
using Case.Net.Parsing;

namespace Case.Net.Common.Conventions;

public class CamelCaseNamingConvention : NamingConvention
{
    private readonly CamelCaseWordEmitter _wordEmitter;
    private readonly CamelCaseParser      _parser;

    public CamelCaseNamingConvention() : base( "camelCase" )
    {
        _wordEmitter = new CamelCaseWordEmitter();
        _parser      = new CamelCaseParser();
    }

    public override bool TryConvert(CasedString input, out CasedString output)
    {
        if ( input.IsEmpty() )
        {
            output = CasedString.Empty;

            return false;
        }

        List<string> words = new List<string>( input.WordCount() );

        for ( int i = 0; i < input.WordCount(); i++ )
        {
            if ( _wordEmitter.EmitWord( input, i, out ReadOnlySpan<char> wordBuffer ) )
            {
                words.Add( wordBuffer.ToString() );
            }
        }

        output = new CasedString( string.Empty, string.Empty, words, EmptyArray<Delimiter>(), this );

        return true;
    }

    public override bool TryParse(ReadOnlySpan<char> input, out CasedString output)
    {
        if ( !_parser.TryParse( input, out IReadOnlyList<WordPosition>? wordPositions ) )
        {
            output = CasedString.Empty;

            return false;
        }

        input.SplitWithWordPositions( wordPositions, out IReadOnlyList<string>? words, out IReadOnlyList<Delimiter>? delimiters );

        output = new CasedString( string.Empty, string.Empty, words, delimiters, this );

        return true;
    }
}
