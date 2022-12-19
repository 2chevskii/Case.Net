using System.Text;

namespace Case.Net.Emit.Sanitizers;

public sealed class PredicateSanitizer : ISanitizer
{
    private readonly Predicate<char> _shouldStripPredicate;

    public PredicateSanitizer(Predicate<char> shouldStripPredicate)
    {
        _shouldStripPredicate = shouldStripPredicate;
    }

    public string SanitizeWord(string word) => Sanitize( word );

    public string SanitizePrefix(string prefix) => Sanitize( prefix );

    public string SanitizeSuffix(string suffix) => Sanitize( suffix );

    private string Sanitize(string input)
    {
        StringBuilder sb = new ();

        for ( var i = 0; i < input.Length; i++ )
        {
            char c = input[i];

            if ( !_shouldStripPredicate( c ) )
                sb.Append( c );
        }

        return sb.ToString();
    }
}
