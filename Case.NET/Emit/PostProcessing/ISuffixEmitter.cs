using System.Collections.Generic;
using System.Text;

using Case.NET.Parsing.Tokens;

namespace Case.NET.Emit.PostProcessing
{
    public interface ISuffixEmitter
    {
        string GetSuffix(IReadOnlyList<WordToken> tokens, string value);

        string GetSuffix(IReadOnlyList<WordToken> tokens, StringBuilder builder);
    }
}
