namespace Case.NET.Parsing.WordSplitting
{
    public interface IWordSplitter
    {
        int TryFindSplitIndex(string value, int startAt, out bool skipIndexChar);
    }
}
