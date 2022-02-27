using System.Collections.Generic;

using Case.NET.Parsing.Tokens;

namespace Case.NET.Emit.PostProcessing
{
    public interface IPrefixEmitter
    {
        string GetPrefix(IList<WordToken> tokens, string value);
    }
}
