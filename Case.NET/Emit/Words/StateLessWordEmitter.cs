using System;
using System.Collections.Generic;

using Case.NET.Parsing.Tokens;

namespace Case.NET.Emit.Words
{
    public class StateLessWordEmitter : IWordEmitter
    {
        public static readonly StateLessWordEmitter AllUpper =
            new StateLessWordEmitter(token => token.Value.ToUpperInvariant());
        public static readonly StateLessWordEmitter AllLower =
            new StateLessWordEmitter(token => token.Value.ToLowerInvariant());
        public static readonly StateLessWordEmitter FirstUpper = new StateLessWordEmitter(
            token => {
                if (token.Value.Length < 2)
                {
                    return token.Value.ToUpperInvariant();
                }

                char firstChar = char.ToUpperInvariant(token.Value[0]);

                string rest = token.Value.Substring(1).ToLowerInvariant();

                return firstChar + rest;
            }
        );
        public static readonly StateLessWordEmitter FirstLower = new StateLessWordEmitter(
            token => {
                if (token.Value.Length < 2)
                {
                    return token.Value.ToLowerInvariant();
                }

                char firstChar = char.ToLowerInvariant(token.Value[0]);

                string rest = token.Value.Substring(1).ToUpperInvariant();

                return firstChar + rest;
            }
        );

        public readonly Func<WordToken, string> EmitterFunc;

        public StateLessWordEmitter(Func<WordToken, string> emitterFunc)
        {
            EmitterFunc = emitterFunc ?? throw new ArgumentNullException(nameof(emitterFunc));
        }

        public string Emit(IReadOnlyList<WordToken> words, int wordIndex)
        {
            return EmitterFunc(words[wordIndex]);
        }
    }
}
