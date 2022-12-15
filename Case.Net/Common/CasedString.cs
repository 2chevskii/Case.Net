using Case.Net.Common.Conventions;

namespace Case.Net.Common;

public readonly struct CasedString
{
    public string Value { get; }
    public IReadOnlyList<Word> Words { get; }
    public INamingConvention Convention { get; }

    public CasedString(
        string value,
        IReadOnlyList<Word> words,
        INamingConvention convention
    )
    {
        Value = value;
        Words = words;
        Convention = convention;
    }
}
