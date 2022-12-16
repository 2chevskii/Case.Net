using Case.Net.Common;
using Case.Net.Parsing.Splitters;
using Case.Net.Parsing.Suffixes;

namespace Case.Net.Parsing.Prefixes;

public class NoParser : IPrefixParser, ISuffixParser, IParser
{
    public static readonly NoParser Instance = new ();

    private NoParser() { }

    public int GetPrefixSize(ReadOnlySpan<char> input) { return 0; }

    public int GetSuffixSize(ReadOnlySpan<char> input) { return 0; }

    public IReadOnlyCollection<IPrefixParser> PrefixParsers => Array.Empty<IPrefixParser>();
    public IReadOnlyCollection<ISuffixParser> SuffixParsers => Array.Empty<ISuffixParser>();
    public IReadOnlyCollection<ISplitter> Splitters => Array.Empty<ISplitter>();

    public CasedString Parse(ReadOnlySpan<char> input) { return CasedString.Empty; }
}
