namespace Case.Net.Common.Entities;

public readonly struct Delimiter
{
    public static readonly Delimiter Empty = new ( -1, string.Empty );

    public readonly int    PreviousWordIndex;
    public readonly string Value;

    public bool IsEmpty => Value is {Length: 0};

    public Delimiter(int previousWordIndex, string value)
    {
        PreviousWordIndex = previousWordIndex;
        Value     = value;
    }

    public override string ToString() => Value;
}
