namespace Case.Net.Common.Conventions;

public interface INamingConvention
{
    string Name { get; }

    CasedString Convert(CasedString source);

    CasedString Parse(ReadOnlySpan<char> input);

    bool TryParse(ReadOnlySpan<char> input,out CasedString output);
}
