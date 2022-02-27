using System.Collections.Generic;

using Case.NET.Parsing.Tokens;

namespace Case.NET.Emit.Words
{
    public class CamelCaseWordEmitter : IWordEmitter
    {
        public string Emit(IReadOnlyList<WordToken> tokens, int index)
        {
            WordToken token = tokens[index];

            if (index == 0)
            {
                return ToFirstLower(token);
            }

            return ToFirstUpper(token);
        }

        string ToFirstLower(WordToken token)
        {
            return token.Value.ToLowerInvariant();
        }

        string ToFirstUpper(WordToken token)
        {
            if (token.Value.Length < 2)
            {
                return token.Value.ToUpperInvariant();
            }

            return char.ToLowerInvariant(token.Value[0]) +
                   token.Value.Substring(1).ToLowerInvariant();
        }
    }
}
