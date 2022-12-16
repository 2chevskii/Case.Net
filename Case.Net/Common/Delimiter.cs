namespace Case.Net.Common;

public readonly struct Delimiter
{
    public int Position { get; }
    public string Value { get; }

    public Delimiter(int position, string value)
    {
        Position = position;
        Value = value;
    }
}
