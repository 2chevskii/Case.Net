using System.Text;

using Case.Net.Common.Conventions;
using Case.Net.Extensions;

namespace Case.Net.Common;

public readonly struct CasedString
{
    public static readonly CasedString Empty = new CasedString(
        string.Empty,
        string.Empty,
        EmptyArray<string>(),
        EmptyArray<Delimiter>(),
        new MixedNamingConvention()
    );

    public string Prefix { get; }
    public string Suffix { get; }
    public IReadOnlyList<string> Words { get; }
    public IReadOnlyList<Delimiter> Delimiters { get; }
    public INamingConvention NamingConvention { get; }

    public CasedString(
        string prefix,
        string suffix,
        IEnumerable<string> words,
        IEnumerable<Delimiter> delimiters,
        INamingConvention namingConvention
    )
    {
        Prefix           = prefix;
        Suffix           = suffix;
        Words            = words.ToArray();
        Delimiters       = delimiters.ToArray();
        NamingConvention = namingConvention;
    }

    public override string ToString()
    {
        /*TODO: Needs cleaner impl*/
        StringBuilder stringBuilder = new ();

        if ( Prefix.Length is not 0 )
        {
            stringBuilder.Append( Prefix );
        }

        for ( var i = 0; i < Words.Count-1; i++ )
        {
            stringBuilder.Append( Words[i] );

            if ( this.HasDelimiterFor( i ) )
            {
                stringBuilder.Append( this.GetDelimiterFor( i ) );
            }
        }

        stringBuilder.Append( Words[^1] );

        if ( Suffix.Length is not 0 )
        {
            stringBuilder.Append( Suffix );
        }

        return stringBuilder.ToString();
    }
}
