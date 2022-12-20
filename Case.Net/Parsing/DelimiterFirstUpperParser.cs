using Case.Net.Extensions;

namespace Case.Net.Parsing;

public class DelimiterFirstUpperParser : IWordParser
{
    public char Delimiter { get; }

    public DelimiterFirstUpperParser(char delimiter)
    {
        Delimiter = delimiter;
    }

    public bool TryParse(ReadOnlySpan<char> input, out IReadOnlyList<WordPosition> words)
    {
        if ( input.IsEmpty )
        {
            words = Array.Empty<WordPosition>();

            return false;
        }

        var wordsRw = new List<WordPosition>();
        words = wordsRw;
        int wordStart = 0;

        if ( !input[0].IsUpper() )
        {
            return false;
        }

        for ( int i = 0; i < input.Length; i++ )
        {
            char current = input[i];

            if ( i == wordStart && !current.IsUpper() && !current.IsDigit() )
            {
                return false;
            }

            if ( i != wordStart && !current.IsLower() && !current.IsDigit() )
            {
                return false;
            }

            if ( input.IsAtEnd( i ) )
            {
                wordsRw.Add( new WordPosition( i, i ) );

                break;
            }

            if ( IsDelimiter( input, i + 1 ) )
            {
                wordsRw.Add( new WordPosition( i, i + 1 ) );
                wordStart = i + 2;
                i++;
            }
        }

        return true;
    }

    bool IsDelimiter(ReadOnlySpan<char> input, int i)
    {
        return input[i] == Delimiter;
    }
}
