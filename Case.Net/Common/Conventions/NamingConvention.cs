namespace Case.Net.Common.Conventions;

public abstract partial class NamingConvention
{
    public abstract string Name { get; }
    public abstract bool CanConvert { get; }

    public abstract CasedString Convert(CasedString source);
}
