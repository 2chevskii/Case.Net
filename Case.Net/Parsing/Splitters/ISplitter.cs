namespace Case.Net.Parsing.Splitters;

public interface ISplitter
{
    bool TryFindSplitIndex(ReadOnlySpan<char> input, ref int wordEnd, ref int nextPosition);
}
