using Case.Net.Common;
using Case.Net.Extensions;

namespace Case.Net.Emit.Words;

public class PascalCaseWordEmitter : IWordEmitter
{
    public static readonly PascalCaseWordEmitter Instance = new ();

    public string EmitWord(CasedString source, int wordIndex)
    {
        var    word      = source.WordAt( wordIndex );
        char[] wordChars = word.ToCharArray();
        wordChars[0] = char.ToUpperInvariant( wordChars[0] );

        for ( int i = 1; i < word.Length; i++ )
        {
            wordChars[i] = char.ToLowerInvariant( wordChars[i] );
        }

        return new string( wordChars );
    }
}
