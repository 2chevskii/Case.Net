using Case.Net.Common.Conventions;
using Case.Net.Extensions;

namespace Case.Net.Common;

public readonly struct CasedString
{
    public static readonly CasedString Empty = new CasedString(
        string.Empty,
        string.Empty,
        Array.Empty<string>(),
        Array.Empty<string>(),
        new MixedNamingConvention()
    );

    public string Prefix { get; }
    public string Suffix { get; }
    public IReadOnlyList<string> Delimiters { get; }
    public IReadOnlyList<string> Words { get; }
    public INamingConvention NamingConvention { get; }

    public bool HasPrefix => !Prefix.IsEmpty();
    public bool HasSuffix => !Suffix.IsEmpty();
    public int WordCount => Words.Count;
    public int DelimiterCount => Delimiters.Count;

    public bool IsEmpty => WordCount is 0;
    public bool IsKnownConvention => NamingConvention is not MixedNamingConvention;

    public CasedString(
        string prefix,
        string suffix,
        IReadOnlyList<string> delimiters,
        IReadOnlyList<string> words,
        INamingConvention namingConvention
    )
    {
        Prefix = prefix;
        Suffix = suffix;
        Delimiters = delimiters;
        Words = words;
        NamingConvention = namingConvention;
    }
}
