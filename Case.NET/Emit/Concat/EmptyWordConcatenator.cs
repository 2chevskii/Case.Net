using System;
using System.Collections.Generic;

using Case.NET.Parsing.Tokens;

namespace Case.NET.Emit.Concat
{
    public class EmptyWordConcatenator : IWordConcatenator
    {
#pragma warning disable CS0618
        public static readonly EmptyWordConcatenator Instance = new EmptyWordConcatenator();
#pragma warning restore CS0618

        [Obsolete("Use static instance instead")]
        public EmptyWordConcatenator() { }

        public string GetConcatenation(IList<WordToken> tokens, int index)
        {
            return string.Empty;
        }
    }
}
