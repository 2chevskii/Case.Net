namespace Case.Net.Common;

public struct Delimiter
{
    public int Index { get; }
    public string Value { get; }

    public int Length => Value.Length;

    public Delimiter(int index, string value)
    {
        Index = index;
        Value = value;
    }
}
