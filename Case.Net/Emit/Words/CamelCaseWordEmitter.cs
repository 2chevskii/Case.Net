using Case.Net.Common;
using Case.Net.Extensions;

namespace Case.Net.Emit.Words;

public class CamelCaseWordEmitter : IWordEmitter
{
    public static readonly CamelCaseWordEmitter Instance = new ();

    public string EmitWord(CasedString source, int wordIndex)
    {
        if ( wordIndex is 0 ) { return source.WordAt( wordIndex ).ToLowerInvariant(); }

        return PascalCaseWordEmitter.Instance.EmitWord( source, wordIndex );
    }
}
