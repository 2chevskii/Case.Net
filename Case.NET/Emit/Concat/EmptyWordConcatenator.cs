using System.Collections.Generic;

using Case.NET.Parsing.Tokens;

namespace Case.NET.Emit.Concat
{
    public class EmptyWordConcatenator : IWordConcatenator
    {

        public string GetConcatenation(IList<WordToken> tokens, int index)
        {
            return string.Empty;
        }
    }
}
