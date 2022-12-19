namespace Case.Net.Common.Conventions;

public interface INamingConvention
{
    string Name { get; }

    bool TryConvert(CasedString input, out CasedString output);

    CasedString Convert(CasedString input);

    CasedString Parse(ReadOnlySpan<char> input);

    bool TryParse(ReadOnlySpan<char> input, out CasedString output);
}
