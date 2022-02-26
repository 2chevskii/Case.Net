using Case.NET.Parsing.Tokens;

namespace Case.NET.Parsing.WordSplitting
{
    public interface IWordSplitter
    {
        bool CanSplit(
            string value,
            int index,
            int bufferedCharCount,
            out bool skipChar,
            out SplitToken splitToken
        );
    }
}
