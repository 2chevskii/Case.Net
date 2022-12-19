namespace Case.Net.Common;

public struct Word
{
    public int Index { get; }
    public string Value { get; }
    public int Length => Value.Length;

    public Word(int index, string value)
    {
        Index  = index;
        Value  = value;
    }

    public override string ToString() => Value;

    public bool StartsWith(char @char) => Value.StartsWith( @char );

    public bool StartWith(string @string) => Value.StartsWith( @string );

    public bool EndsWith(char @char) => Value.EndsWith( @char );

    public bool EndsWith(string @string) => Value.EndsWith( @string );
}
