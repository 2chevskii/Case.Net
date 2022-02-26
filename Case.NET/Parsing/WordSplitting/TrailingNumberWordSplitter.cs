namespace Case.NET.Parsing.WordSplitting
{
    public class TrailingNumberWordSplitter : IWordSplitter
    {
        public int TryFindSplitIndex(string value, int startAt, out bool skipIndexChar)
        {
            skipIndexChar = false;

            for (int i = startAt + 1; i < value.Length; i++)
            {
                char c = value[i];
                char cPrev = value[i - 1];

                if (!char.IsDigit(cPrev) || !char.IsLetter(c))
                {
                    continue;
                }

                return i;
            }

            return -1;
        }
    }
}
