namespace Case.Net.Common.Conventions;

public interface INamingConvention
{
    string Name { get; }

    CasedString Convert(CasedString source);
}
