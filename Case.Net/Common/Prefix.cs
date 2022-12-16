using Case.Net.Parsing.Prefixes;

namespace Case.Net.Common;

public readonly struct Prefix
{
    public static readonly Prefix Empty = new ( NoParser.Instance, string.Empty );
    public IPrefixParser Parser { get; }
    public string Value { get; }
    public bool IsEmpty => Value.Length is 0;

    public Prefix(IPrefixParser parser, string value)
    {
        Parser = parser;
        Value = value;
    }
}
