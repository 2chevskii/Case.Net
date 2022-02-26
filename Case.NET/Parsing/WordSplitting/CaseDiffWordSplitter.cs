using Case.NET.Parsing.Tokens;

namespace Case.NET.Parsing.WordSplitting
{
    public class CaseDiffWordSplitter : IWordSplitter
    {
        public virtual bool CanSplit(
            string value,
            int index,
            int bufferedCharCount,
            out bool skipChar,
            out SplitToken splitToken
        )
        {
            char c = value[index];

            if (!char.IsLetterOrDigit(c))
            {
                splitToken = default;
                skipChar = false;

                return false;
            }

            CaseType cCase = GetCharCase(c);

            CaseType pcCase = GetPreviousCharCase(value, index);

            if (pcCase == CaseType.NONE || pcCase == cCase)
            {
                splitToken = default;
                skipChar = false;

                return false;
            }

            splitToken = new SplitToken(
                index,
                string.Empty,
                0,
                false,
                true
            );

            skipChar = false;

            return true;
        }

        protected CaseType GetPreviousCharCase(string value, int index)
        {
            if (index == 0)
            {
                return CaseType.NONE;
            }

            return GetCharCase(value[index - 1]);
        }

        protected CaseType GetNextCharCase(string value, int index)
        {
            if (index == value.Length - 1)
            {
                return CaseType.NONE;
            }

            return GetCharCase(value[index + 1]);
        }

        protected CaseType GetCharCase(char c) => char.IsUpper(c)
                                                      ? CaseType.UPPER
                                                      : char.IsLower(c)
                                                          ? CaseType.LOWER
                                                          : CaseType.NONE; 

        public enum CaseType {NONE, UPPER, LOWER }
    }
}
