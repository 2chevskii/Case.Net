using Case.Net.Common.Entities;
using Case.Net.Emitters.Delimiters;
using Case.Net.Emitters.Sanitizers;
using Case.Net.Emitters.Words;
using Case.Net.Extensions;
using Case.Net.Parsing;

namespace Case.Net.Common.Conventions;

public class KebabCaseNamingConvention : NamingConvention
{
    private readonly AllLowerWordEmitter        _wordEmitter;
    private readonly SingleCharDelimiterEmitter _delimiterEmitter;

    public KebabCaseNamingConvention() : base( "kebab-case" )
    {
        _wordEmitter      = new AllLowerWordEmitter( new LetterOrDigitSanitizer() );
        _delimiterEmitter = new SingleCharDelimiterEmitter( '-', false );
        /*
         * we dont really need to waste CPU cycles on checking word start/end
         * before emitting the delimiter
         * because sanitizer won't allow dash character anyway
         */
    }

    public override bool TryConvert(CasedString input, out CasedString output)
    {
        if ( input.IsEmpty() )
        {
            output = CasedString.Empty;

            return false;
        }

        List<string> words = new ();

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
            List<Delimiter> delimiterList = new ();

            for ( int i = 0; i < words.Count - 1; i++ )
            {
                if ( _delimiterEmitter.EmitDelimiter(words, i, out ReadOnlySpan<char> delimiterBuffer) )
                {
                    Delimiter delimiter = new Delimiter( i, delimiterBuffer.ToString() );
                    delimiterList.Add( delimiter );
                }
            }

            delimiters = delimiterList;
        }
        else
        {
            delimiters = Array.Empty<Delimiter>();
        }

        output = new CasedString( string.Empty, string.Empty, words, delimiters, this );

        return true;
    }

    public override bool TryParse(ReadOnlySpan<char> input, out CasedString output)
    {
        if ( !new KebabCaseParser().TryParse( input, out IReadOnlyList<WordPosition>? wordPositions ) )
        {
            output = CasedString.Empty;

            return false;
        }

        input.SplitWithWordPositions( wordPositions, out IReadOnlyList<string>? words, out IReadOnlyList<Delimiter>? delimiters );

        output = new CasedString( string.Empty, string.Empty, words, delimiters, this );

        return true;
    }
}
