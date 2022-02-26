using Case.NET.Parsing.Tokens;

namespace Case.NET.Parsing.WordSplitting
{
    public abstract class VariableCharWordSplitter : IWordSplitter
    {
        public abstract char[] SplitChars { get; }

        public bool CanSplit(
            string value,
            int index,
            int bufferedCharCount,
            out bool skipChar,
            out SplitToken splitToken
        )
        {
            char c = value[index];

            if (!Utils.Contains(SplitChars,c))
            {
                skipChar = false;
                splitToken = default;

                return false;
            }

            char pc = index != 0 ? value[index - 1] : char.MinValue;

            if (!char.IsLetterOrDigit(pc))
            {
                skipChar = true;
                splitToken = new SplitToken(
                    index,
                    c.ToString(),
                    1,
                    true,
                    false
                );

                return false;
            }

            skipChar = true;
            splitToken = new SplitToken(
                index,
                c.ToString(),
                1,
                true,
                true
            );

            return true;
        }
    }
}
