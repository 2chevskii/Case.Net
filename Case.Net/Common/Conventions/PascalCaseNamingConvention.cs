using Case.Net.Emit.Words;
using Case.Net.Extensions;
using Case.Net.Parsing;

namespace Case.Net.Common.Conventions;

public class PascalCaseNamingConvention : NamingConvention
{
    public PascalCaseNamingConvention() : base( "PascalCase" ) { }

    public override bool TryConvert(CasedString input, out CasedString output)
    {
        throw new NotImplementedException();
    }

    public override bool TryParse(ReadOnlySpan<char> input, out CasedString output)
    {
        if ( !new PascalCaseParser().TryParse( input, out var wordPositions ) )
        {
            output = CasedString.Empty;

            return false;
        }

        input.SplitWithWordPositions( wordPositions, out var words, out var delimiters );

        var cs = new CasedString( string.Empty, string.Empty, words, delimiters, this );

        output = cs;

        return true;
    }
}
