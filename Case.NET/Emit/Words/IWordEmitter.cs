using System.Collections.Generic;

using Case.NET.Parsing.Tokens;

namespace Case.NET.Emit.Words
{
    public interface IWordEmitter
    {
        string Emit(IList<WordToken> words, int wordIndex);
    }
}
