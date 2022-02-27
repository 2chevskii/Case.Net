using System.Collections.Generic;

using Case.NET.Parsing.Tokens;

namespace Case.NET.Emit.Words
{
    public interface IWordEmitter
    {
        string Emit(IReadOnlyList<WordToken> words, int wordIndex);
    }
}
