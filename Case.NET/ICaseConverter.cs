using System.Collections.Generic;

using Case.NET.Emit.Concat;
using Case.NET.Emit.PostProcessing;
using Case.NET.Emit.Words;
using Case.NET.Parsing;
using Case.NET.Parsing.Tokens;

namespace Case.NET
{
    public interface ICaseConverter
    {
        IParser Parser { get; }
        IWordEmitter WordEmitter { get; }
        IWordConcatenator WordConcatenator { get; }
        IPrefixEmitter PrefixEmitter { get; }
        ISuffixEmitter SuffixEmitter { get; }

        /// <summary>
        /// Converts given <paramref name="value"/> into <see cref="CasedString"/>
        /// using present parsers and emitters
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        CasedString ConvertCase(string value);

        /// <summary>
        /// Does the same as <see cref="ConvertCase(string)"/>, but skips parsing stage
        /// using already parsed <paramref name="tokens"/>
        /// </summary>
        /// <param name="tokens"></param>
        /// <param name="originalValue">Used only for constructing <see cref="CasedString"/> (<see cref="CasedString.OriginalValue"/>)</param>
        /// <returns></returns>
        CasedString ConvertCase(IReadOnlyList<WordToken> tokens, string originalValue);
    }
}
