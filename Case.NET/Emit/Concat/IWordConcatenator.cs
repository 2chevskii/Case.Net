using System.Collections.Generic;

using Case.NET.Parsing.Tokens;

namespace Case.NET.Emit.Concat
{
    public interface IWordConcatenator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tokens"></param>
        /// <param name="index">Index of the token, AFTER which concatenation will be performed (cannot include last token)</param>
        /// <returns></returns>
        string GetConcatenation(IReadOnlyList<WordToken> tokens, int index);
    }
}
