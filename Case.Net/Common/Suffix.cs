using Case.Net.Parsing.Prefixes;
using Case.Net.Parsing.Suffixes;

namespace Case.Net.Common;

public readonly struct Suffix
{
    public static readonly Suffix Empty = new Suffix( NoParser.Instance, string.Empty );

    public ISuffixParser Parser { get; }
    public string Value { get; }

    public bool IsEmpty => Value.Length is 0;

    public Suffix(ISuffixParser parser, string value)
    {
        Parser = parser;
        Value = value;
    }
}
