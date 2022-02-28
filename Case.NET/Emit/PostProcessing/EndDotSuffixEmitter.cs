using System.Collections.Generic;
using System.Text;

using Case.NET.Parsing.Tokens;

namespace Case.NET.Emit.PostProcessing
{
    public class EndDotSuffixEmitter : ISuffixEmitter
    {

        public string GetSuffix(IReadOnlyList<WordToken> tokens, string value)
        {
            if (!value.EndsWith("."))
            {
                return ".";
            }

            return string.Empty;
        }

        public string GetSuffix(IReadOnlyList<WordToken> tokens, StringBuilder builder)
        {
            if (builder[builder.Length - 1] != '.')
            {
                return ".";
            }

            return string.Empty;
        }
    }
}
