using Case.Net.Emit.Words;
using Case.Net.Extensions;

namespace Case.Net.Common.Conventions;

public class PascalCaseNamingConvention : INamingConvention
{
    public string Name => "PascalCase";

    public CasedString Convert(CasedString source)
    {
        List<string> words = new ();

        for ( var i = 0; i < source.WordCount(); i++ )
        {
            words.Add(PascalCaseWordEmitter.Instance.EmitWord(source, i));
        }

        // return new CasedString( words, this );
        throw new NotImplementedException();
    }

    public CasedString Parse(ReadOnlySpan<char> input)
    {
        if ( !TryParse( input, out var output ) )
        {
            throw new Exception();
        }

        return output;
    }

    public bool TryParse(ReadOnlySpan<char> input, out CasedString output)
    {
        /*output = CasedString.Empty;

        if ( input.Length is 0 )
            return false;

        if ( !char.IsUpper( input[0] ) )
            return false;

        if ( input.Length is 1 )
        {
            output = new CasedString( new[] {input.ToString()}, this );

            return true;
        }

        List<string> words             = new ();
        int          wordStartPosition = 0;

        for ( int i = 0; i < input.Length; i++ )
        {
            char current = input[i];

            if ( !char.IsLetterOrDigit( current ) )
            {
                return false;
            }

            if ( i == input.Length - 1 )
            {
                words.Add(input.Slice(wordStartPosition).ToString());

                break;
            }

            char next = input[i + 1];

            bool isWordEnd   = char.IsLower( current ) || char.IsDigit( current );
            bool isWordStart = char.IsUpper( next );

            if ( isWordEnd && isWordStart )
            {
                var wordLength = i - wordStartPosition + 1;
                words.Add( input.Slice( wordStartPosition, wordLength ).ToString() );
                wordStartPosition += wordLength;
            }
        }

        output = new CasedString( words, this );

        return true;*/

        throw new NotImplementedException();
    }
}
