using System.Collections.Generic;
using System.Text;

using Case.NET.Parsing.Tokens;

namespace Case.NET.Emit.PostProcessing
{
    public interface IPrefixEmitter
    {
        string GetPrefix(IList<WordToken> tokens, string value);

        // TODO: Implement
        //string GetPrefix(IList<WordToken> tokens, StringBuilder builder);
    }
}
