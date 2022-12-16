using System.Diagnostics.Tracing;

namespace Case.Net.Common.Conventions;

public class CamelCaseNamingConvention : INamingConvention
{
    public string Name => "camelCase";

    public CasedString Convert(CasedString source)
    {
        if ( source.WordCount is 0 )
        {
            /*return empty casedstring*/
            return CasedString.Empty;
        }

        var words = new List<string>();

        var    w1 = source.Words[0];
        Span<char> b1 = stackalloc char[w1.Length];
        w1.CopyTo( b1 );
        Lower( b1 );

        words.Add(b1.ToString());

        for ( var i = 1; i < source.Words.Count; i++ )
        {
            var        word   = source.Words[i];
            Span<char> buffer = stackalloc char[word.Length]; // TODO: Test this
            word.CopyTo( buffer );
            Capitalize( buffer );
            words.Add(buffer.ToString());
        }

        return new CasedString( string.Empty, string.Empty, Array.Empty<string>(), words, this );
    }

    public CasedString Parse(ReadOnlySpan<char> input)
    {
        if ( !TryParse( input, out CasedString output ) )
        {
            throw new Exception( "Failed to parse input" );
        }

        return output;
    }

    public bool TryParse(ReadOnlySpan<char> input, out CasedString output)
    {
        List<int> splitPositions = new ();

        for ( int i = 0; i < input.Length - 1; i++ )
        {
            var current = input[i];
            var next    = input[i + 1];

            if ( !char.IsLower( current ) || !char.IsUpper(next))
            {
                continue;
            }

            splitPositions.Add( i );
        }

        List<string> words = new ();

        if ( !splitPositions.Any() )
        {
            words.Add( input.ToString() );
        }
        else
        {
            for ( var i = 0; i < splitPositions.Count; i++ )
            {

            }
        }
    }

    void Capitalize(Span<char> buffer)
    {
        if ( buffer.Length is 0 )
            return;

        buffer[0] = char.ToUpperInvariant( buffer[0] );

        for ( int i = 1; i < buffer.Length; i++ )
        {
            buffer[i] = char.ToLowerInvariant( buffer[i] );
        }
    }

    void Lower(Span<char> buffer)
    {
        for ( int i = 0; i < buffer.Length; i++ )
        {
            buffer[i] = char.ToLowerInvariant( buffer[i] );
        }
    }
}
