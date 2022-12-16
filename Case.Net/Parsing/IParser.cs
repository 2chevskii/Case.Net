using Case.Net.Common;
using Case.Net.Parsing.Prefixes;
using Case.Net.Parsing.Splitters;
using Case.Net.Parsing.Suffixes;

namespace Case.Net.Parsing;

public interface IParser
{
    IReadOnlyCollection<IPrefixParser> PrefixParsers { get; }
    IReadOnlyCollection<ISuffixParser> SuffixParsers { get; }
    IReadOnlyCollection<ISplitter> Splitters { get; }

    CasedString Parse(ReadOnlySpan<char> input);
}
