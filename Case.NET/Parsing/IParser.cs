using System.Collections.Generic;

using Case.NET.Parsing.Filters;
using Case.NET.Parsing.Tokens;
using Case.NET.Parsing.WordSplitting;

namespace Case.NET.Parsing
{
    /// <summary>
    /// Type, implementing parser for string value, which produces sequence of <see cref="IToken"/>
    /// </summary>
    public interface IParser
    {
        /// <summary>
        /// Contains all <see cref="IWordSplitter"/> objects, used by this parser
        /// </summary>
        IReadOnlyCollection<IWordSplitter> WordSplitters { get; }

        /// <summary>
        /// Contains all <see cref="ICharFilter"/> objects, used by this parser
        /// </summary>
        IReadOnlyCollection<ICharFilter> CharFilters { get; }

        IList<IToken> Parse(string value);
    }
}
