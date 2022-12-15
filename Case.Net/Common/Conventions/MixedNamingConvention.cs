namespace Case.Net.Common.Conventions;

public class MixedNamingConvention : NamingConvention
{
    public override string Name => "Mixed";

    public override CasedString Convert(CasedString source)
    {
        return source;
    }
}
