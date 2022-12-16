namespace Case.Net.Parsing.Splitters;

public interface ISplitter
{
    SplitResult Split(ReadOnlySpan<char> input);
}
