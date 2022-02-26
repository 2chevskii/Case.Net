using System;

using Case.NET.Parsing.Tokens;

namespace Case.NET.Parsing.WordSplitting
{
    public abstract class SequenceWordSplitter : IWordSplitter
    {

        public bool CanSplit(
            string value,
            int index,
            int bufferedCharCount,
            out bool skipChar,
            out SplitToken splitToken
        )
        {
            throw new NotImplementedException($"Method {nameof(CanSplit)} for {nameof(SequenceWordSplitter)} was not implemented");
        }
    }
}
