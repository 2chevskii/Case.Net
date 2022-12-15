namespace Case.Net.Common.Conventions;

public class MixedNamingConvention : INamingConvention
{
    public string Name => "Mixed";

    public CasedString Convert(CasedString source) => source;
}
