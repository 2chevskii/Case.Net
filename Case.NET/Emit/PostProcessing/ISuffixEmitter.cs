using System.Collections.Generic;

using Case.NET.Parsing.Tokens;

namespace Case.NET.Emit.PostProcessing
{
    public interface ISuffixEmitter
    {
        string GetSuffix(IList<WordToken> tokens, string value);
    }
}
