namespace Case.Net.Common;

public readonly struct Delimiter
{
    public readonly int    PreviousWordIndex;
    public readonly string Value;

    public Delimiter(int previousWordIndex, string value)
    {
        PreviousWordIndex = previousWordIndex;
        Value     = value;
    }

    public override string ToString() => Value;
}
