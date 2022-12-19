using System.Text;

using Case.Net.Common.Conventions;
using Case.Net.Extensions;

namespace Case.Net.Common;

public readonly struct CasedString
{
    public static readonly CasedString Empty = new CasedString(
        string.Empty,
        string.Empty,
        Array.Empty<Delimiter>(),
        Array.Empty<Word>(),
        new MixedNamingConvention()
    );

    public string Prefix { get; }
    public string Suffix { get; }
    public IReadOnlyList<Word> Words { get; }
    public IReadOnlyList<Delimiter> Delimiters { get; }
    public INamingConvention NamingConvention { get; }

    public CasedString(
        string prefix,
        string suffix,
        IReadOnlyList<Delimiter> delimiters,
        IReadOnlyList<Word> words,
        INamingConvention namingConvention
    )
    {
        Prefix           = prefix;
        Suffix           = suffix;
        Delimiters       = delimiters;
        Words            = words;
        NamingConvention = namingConvention;
    }

    public CasedString(IReadOnlyList<Word> words, INamingConvention namingConvention)
    {
        Prefix           = string.Empty;
        Suffix           = string.Empty;
        Words            = words;
        Delimiters       = Array.Empty<Delimiter>();
        NamingConvention = namingConvention;
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append( Prefix );

        for ( int i = 0; i < Words.Count - 1; i++ )
        {
            sb.Append( Words[i] );
            sb.Append( Delimiters[0] );
        }

        sb.Append( Words[0] );
        sb.Append( Suffix );

        return sb.ToString();
    }
}
