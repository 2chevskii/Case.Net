using System.Text;

using Case.Net.Extensions;

namespace Case.Net.Common.Conventions;

public class SnakeCaseNamingConvention : INamingConvention
{
    public string Name => "snake_case";

    public CasedString Parse(ReadOnlySpan<char> input)
    {
        if ( !TryParse( input, out var output ) )
            throw new Exception();

        return output;
    }

    public bool TryParse(ReadOnlySpan<char> input, out CasedString output)
    {
        throw new NotImplementedException();

        /*output = CasedString.Empty;

        if ( input.Length is 0 )
        {
            return false;
        }

        if ( !char.IsLower( input[0] ) )
            return false;

        if ( input.Length is 1 )
        {
            output = new CasedString( new[] {input.ToString()}, this );

            return true;
        }

        List<string> words             = new ();
        List<char>   delimiters        = new ();
        int          wordStartPosition = 0;

        for ( int i = 0; i < input.Length; i++ )
        {
            char current = input[i];

            /*if ( !char.IsLetterOrDigit( current ) || !char.IsLower(current) )
            {
                return false;
            }#1#

            if ( !current.IsDigit() && current.IsLetter() && !char.IsLower( current ) )
            {
                return false;
            }

            if ( i == input.Length - 1 )
            {
                words.Add(input.Slice(wordStartPosition).ToString());

                break;
            }

            char next = input[i + 1];

            bool isDelimiter = next.IsUnderscore();

            if ( isDelimiter )
            {
                words.Add(input.Slice(wordStartPosition, i-wordStartPosition+1).ToString());
                delimiters.Add( next );
                wordStartPosition = i + 2;
                i++;
            }
        }

        output = new CasedString(
            string.Empty,
            string.Empty,
            delimiters.Select( x => x.ToString() ).ToList(),
            words,
            this
        );

        return true;*/
    }

    public CasedString Convert(CasedString input)
    {
        List<string> words      = new ();
        List<string> delimiters = new ();

        for ( var i = 0; i < input.Words.Count; i++ )
        {
            var word = input.WordAt( i ).ToLowerInvariant();

            words.Add( word );

            if ( input.WordCount() - i > 1 )
            {
                delimiters.Add('_'.ToString());
            }
        }

        throw new NotImplementedException();

        // return new CasedString( string.Empty, string.Empty, delimiters, words, this );
    }
}
