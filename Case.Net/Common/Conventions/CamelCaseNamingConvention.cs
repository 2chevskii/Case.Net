using Case.Net.Emit.Words;
using Case.Net.Extensions;
using Case.Net.Parsing;

namespace Case.Net.Common.Conventions;

public class CamelCaseNamingConvention : NamingConvention
{
    private readonly IWordEmitter _wordEmitter = CamelCaseWordEmitter.Instance;

    private readonly CamelCaseParser _parser = new ();

    public CamelCaseNamingConvention() : base( "camelCase" ) { }

    public override bool TryConvert(CasedString input, out CasedString output)
    {
        throw new NotImplementedException();
    }

    public override bool TryParse(ReadOnlySpan<char> input, out CasedString output)
    {
        if ( !_parser.TryParse( input, out var wordPositions ) )
        {
            output = CasedString.Empty;

            return false;
        }

        input.SplitWithWordPositions( wordPositions, out var words, out var delimiters );

        output = new CasedString( string.Empty, string.Empty, words, delimiters, this );

        return true;
    }
}
