using Case.Net.Common.Conventions;

namespace Case.Net.Common;

public readonly struct CasedString
{
    public string Value { get; }
    public string Prefix { get; }
    public string Suffix { get; }
    public IReadOnlyList<string> Concatenators { get; }
    public IReadOnlyList<Word> Words { get; }
    public INamingConvention Convention { get; }

    public CasedString(
        string value,
        string prefix,
        string suffix,
        IReadOnlyList<Word> words,
        INamingConvention convention
    )
    {
        Value      = value;
        Prefix     = prefix;
        Suffix     = suffix;
        Words      = words;
        Convention = convention;
    }
}
