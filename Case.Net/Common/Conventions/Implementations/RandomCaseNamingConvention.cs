using Case.Net.Common.Entities;

namespace Case.Net.Common.Conventions;

public class RandomCaseNamingConvention : NamingConvention
{

    public RandomCaseNamingConvention(string name) : base( name ) { }

    public override bool TryConvert(CasedString input, out CasedString output)
    {
        var words    = input.Words;
        var wordsOut = new string[words.Count];
        for ( var i = 0; i < words.Count; i++ )
        {
            var word   = words[i];
            var buffer = new char[word.Length];
            for ( var j = 0; j < word.Length; j++ )
            {
                buffer[j] = IsUpper()
                ? char.ToUpperInvariant( word[j] )
                : char.ToLowerInvariant( word[j] );
            }

            wordsOut[i] = new string( buffer );
        }


    }

    static bool IsUpper()
    {
        return Random.Shared.Next( 2 ) > 0;
    }

    public override bool TryParse(ReadOnlySpan<char> input, out CasedString output)
    {
        Parse( input ); // throws
        output = CasedString.Empty;
        return false;
    }

    public override CasedString Parse(ReadOnlySpan<char> input)
    {
        throw new InvalidOperationException(
            $"{nameof( RandomCaseNamingConvention )} is unable to parse"
        );
    }
}
