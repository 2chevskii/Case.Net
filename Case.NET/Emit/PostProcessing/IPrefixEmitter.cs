using System.Collections.Generic;
using System.Text;

using Case.NET.Parsing.Tokens;

namespace Case.NET.Emit.PostProcessing
{
    public interface IPrefixEmitter
    {
        string GetPrefix(IReadOnlyList<WordToken> tokens, string value);

        string GetPrefix(IReadOnlyList<WordToken> tokens, StringBuilder builder);
    }
}
