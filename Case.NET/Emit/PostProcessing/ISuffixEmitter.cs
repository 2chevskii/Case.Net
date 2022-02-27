using System.Collections.Generic;
using System.Text;

using Case.NET.Parsing.Tokens;

namespace Case.NET.Emit.PostProcessing
{
    public interface ISuffixEmitter
    {
        string GetSuffix(IList<WordToken> tokens, string value);

        // TODO: Implement
        //string GetSuffix(IList<WordToken> tokens, StringBuilder builder);
    }
}
