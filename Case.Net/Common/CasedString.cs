using System.Text;

using Case.Net.Common.Conventions;
using Case.Net.Extensions;

namespace Case.Net.Common;

public readonly struct CasedString
{
    public static readonly CasedString Empty = new CasedString(
        /*string.Empty,
        string.Empty,
        Array.Empty<string>(),
        Array.Empty<string>(),
        new MixedNamingConvention()*/
    );

    public string Prefix { get; }
    public string Suffix { get; }
    public IReadOnlyList<string> Words { get; }
    public IReadOnlyList<string> Delimiters { get; }
    public INamingConvention NamingConvention { get; }

    public CasedString(string prefix, string suffix, IEnumerable<string> words, IEnumerable<string> delimiters, INamingConvention namingConvention)
    {
        Prefix           = prefix;
        Suffix           = suffix;
        Words            = words.ToArray();
        Delimiters       = delimiters.ToArray();
        NamingConvention = namingConvention;
    }
}
