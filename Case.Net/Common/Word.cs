using System.Diagnostics;

using Case.Net.Parsing.Splitters;

namespace Case.Net.Common;

[DebuggerDisplay( "{Position}: \"{Value}\"" )]
public readonly struct Word : IEquatable<Word>
{
    public Position Position { get; }
    public string Value { get; }
    public ISplitter? Splitter { get; }

    public Word(Position position, string value, ISplitter? splitter)
    {
        Position = position;
        Value    = value;
        Splitter = splitter;
    }

    public bool Equals(Word other) =>
    Position.Equals( other.Position ) && Value == other.Value && Equals( Splitter, other.Splitter );

    public override bool Equals(object? obj) => obj is Word other && Equals( other );

    public override int GetHashCode()
    {
        if ( Splitter is not null )
        {
            return HashCode.Combine(
                Position.GetHashCode(),
                Value.GetHashCode(),
                Splitter.GetHashCode()
            );
        }

        return HashCode.Combine( Position.GetHashCode(), Value.GetHashCode() );
    }
}
