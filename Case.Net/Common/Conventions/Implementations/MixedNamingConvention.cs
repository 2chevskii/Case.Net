using Case.Net.Common.Entities;

namespace Case.Net.Common.Conventions;

public class MixedNamingConvention : INamingConvention
{
    public string Name => "Mixed";

    public bool TryConvert(CasedString input, out CasedString output)
    {
        output = input;

        return false;
    }

    public CasedString Convert(CasedString source)
    {
        throw new InvalidOperationException();
    }

    public CasedString Parse(ReadOnlySpan<char> input)
    {
        if ( !TryParse( input, out CasedString output ) )
        {
            throw new Exception();
        }

        return output;
    }

    public bool TryParse(ReadOnlySpan<char> input, out CasedString output) =>
    throw new NotImplementedException();
}
