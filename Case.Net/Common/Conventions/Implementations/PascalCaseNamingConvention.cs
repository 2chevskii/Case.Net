using Case.Net.Common.Entities;
using Case.Net.Emitters.Sanitizers;
using Case.Net.Emitters.Words;
using Case.Net.Extensions;
using Case.Net.Parsing;

namespace Case.Net.Common.Conventions;

public class PascalCaseNamingConvention : NamingConvention
{
    private readonly IWordEmitter _wordEmitter;

    public PascalCaseNamingConvention() : base( "PascalCase" )
    {
        _wordEmitter = new FirstUpperWordEmitter( new LetterOrDigitSanitizer() );
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

        output = new CasedString(
            string.Empty,
            string.Empty,
            words,
            EmptyArray<Delimiter>(),
            this
        );

        return true;
    }

    public override bool TryParse(ReadOnlySpan<char> input, out CasedString output)
    {
        if ( !new PascalCaseParser().TryParse( input, out IReadOnlyList<WordPosition>? wordPositions ) )
        {
            output = CasedString.Empty;

            return false;
        }

        input.SplitWithWordPositions( wordPositions, out IReadOnlyList<string>? words, out IReadOnlyList<Delimiter>? delimiters );

        CasedString cs = new CasedString( string.Empty, string.Empty, words, delimiters, this );

        output = cs;

        return true;
    }
}
