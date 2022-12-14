using System.Diagnostics;

namespace Case.Net.Common;

[DebuggerDisplay("[{Start}-{End}, {Length}]")]
public readonly struct Position : IEquatable<Position>
{
    public int Start { get; }
    public int End { get; }
    public int Length => End - Start + 1;

    public Position(int start, int end)
    {
        Start = start;
        End   = end;
    }

    public bool Equals(Position other) => Start == other.Start && End == other.End;

    public override bool Equals(object? obj) => obj is Position other && Equals( other );

    public override int GetHashCode() => HashCode.Combine(Start,End );
}
