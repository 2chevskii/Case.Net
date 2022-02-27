using System.Collections.Generic;
using System.Text;

using Case.NET.Parsing.Tokens;

namespace Case.NET.Emit.PostProcessing
{
    public class UnderscorePrefixEmitter : IPrefixEmitter
    {
        private const string UNDERSCORE_STR  = "_";
        private const char   UNDERSCORE_CHAR = '_';

        public string GetPrefix(IReadOnlyList<WordToken> tokens, string value)
        {
#if NETSTANDARD2_1_OR_GREATER
            if (!value.StartsWith(UNDERSCORE_CHAR))
#else
            if(!value.StartsWith(UNDERSCORE_STR))
#endif
            {
                return UNDERSCORE_STR;
            }

            return string.Empty;
        }

        public string GetPrefix(IReadOnlyList<WordToken> tokens, StringBuilder builder)
        {
            if (!builder[0].Equals(UNDERSCORE_CHAR))
            {
                return UNDERSCORE_STR;
            }

            return string.Empty;
        }
    }
}
