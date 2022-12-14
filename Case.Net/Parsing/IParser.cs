using Case.Net.Common;
using Case.Net.Parsing.CharFilters;
using Case.Net.Parsing.Splitters;

namespace Case.Net.Parsing;

public interface IParser
{
    IReadOnlyCollection<ICharFilter> CharFilters { get; }
    IReadOnlyCollection<ISplitter> Splitters { get; }

    IReadOnlyCollection<Word> Parse(ReadOnlySpan<char> input);
}
