using System.Collections.Generic;

using Case.NET.Parsing.Tokens;

namespace Case.NET.Emit.PostProcessing
{
    public class UnderscorePrefixEmitter : IPrefixEmitter
    {
        private const string UNDERSCORE_STR  = "_";
        private const char   UNDERSCORE_CHAR = '_';

        public string GetPrefix(IList<WordToken> tokens, string value)
        {
#if NETSTANDARD2_1_OR_GREATER
            if (value.StartsWith(UNDERSCORE_CHAR))
#else
            if(value.StartsWith(UNDERSCORE_STR))
#endif
            {
                return UNDERSCORE_STR;
            }

            return string.Empty;
        }
    }
}
