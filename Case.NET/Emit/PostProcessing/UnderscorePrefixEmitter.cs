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
            if (!value.StartsWith(UNDERSCORE_CHAR))
            {
                return UNDERSCORE_STR;
            }

            return string.Empty;
        }
    }
}
