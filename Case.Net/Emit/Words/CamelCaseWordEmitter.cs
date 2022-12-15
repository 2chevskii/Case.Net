using Case.Net.Common;
using Case.Net.Extensions;

namespace Case.Net.Emit.Words;

public class CamelCaseWordEmitter : IWordEmitter
{
    public string EmitWord(CasedString source, int wordIndex)
    {
        Word       word  = source.WordAt( wordIndex );
        Span<char> value = new Span<char>( null, 0, word.Value.Length );

        if ( source.IsFirst( word ) )
        {
            for ( int i = 0; i < word.Value.Length; i++ )
            {
                value[i] = char.ToLowerInvariant( word.Value[i] );
            }
        }
        else
        {
            value[0] = char.ToLowerInvariant( word.Value[0] );
            for ( int i = 1; i < word.Value.Length; i++ )
            {
                value[i] = char.ToLowerInvariant( word.Value[i] );
            }
        }

        return new string( value );
    }
}
