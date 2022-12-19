namespace Case.Net.Common.Conventions;

public class MixedNamingConvention : INamingConvention
{
    public string Name => "Mixed";

    public CasedString Convert(CasedString source) => source;

    public CasedString Parse(ReadOnlySpan<char> input) => throw new NotImplementedException();

    public bool TryParse(ReadOnlySpan<char> input, out CasedString output) =>
    throw new NotImplementedException();
}
