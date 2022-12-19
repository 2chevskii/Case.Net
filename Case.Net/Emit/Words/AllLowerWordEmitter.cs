using Case.Net.Common;
using Case.Net.Extensions;

namespace Case.Net.Emit.Words;

public class AllLowerWordEmitter : IWordEmitter
{
    public string EmitWord(CasedString source, int wordIndex)
    {
        var word = source.WordAt( wordIndex );

        return word;
    }
}
