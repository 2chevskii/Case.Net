using System;
using System.Collections.Generic;

using Case.NET.Parsing.Tokens;

namespace Case.NET.Emit.Words
{
    public class CamelCaseWordEmitter : IWordEmitter
    {
#pragma warning disable CS0618
        public static readonly CamelCaseWordEmitter Instance = new CamelCaseWordEmitter();
#pragma warning restore CS0618

        [Obsolete("Use static instance instead")]
        public CamelCaseWordEmitter() { }

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

            return char.ToUpperInvariant(token.Value[0]) +
                   token.Value.Substring(1).ToLowerInvariant();
        }
    }
}
