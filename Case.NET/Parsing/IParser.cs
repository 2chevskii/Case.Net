using System.Collections.Generic;

using Case.NET.Parsing.Filters;
using Case.NET.Parsing.Tokens;
using Case.NET.Parsing.WordSplitting;

namespace Case.NET.Parsing
{
    public interface IParser
    {
        IReadOnlyCollection<IWordSplitter> WordSplitters { get; }
        IReadOnlyCollection<ICharFilter> CharFilters { get; }

        ICollection<IToken> Parse(string value, bool includeSplitTokens, bool returnSourceIfNoMatches);
    }
}
