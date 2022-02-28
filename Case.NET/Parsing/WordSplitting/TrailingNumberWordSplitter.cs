using System;

namespace Case.NET.Parsing.WordSplitting
{
    public class TrailingNumberWordSplitter : IWordSplitter
    {
#pragma warning disable CS0618
        public static readonly TrailingNumberWordSplitter Instance =
            new TrailingNumberWordSplitter();
#pragma warning restore CS0618

        [Obsolete("Use static instance instead")]
        public TrailingNumberWordSplitter() { }

        public int TryFindSplitIndex(string value, int startAt, out bool skipIndexChar)
        {
            skipIndexChar = false;

            // skip leading digits
            while (startAt < value.Length && char.IsDigit(value[startAt]))
            {
                startAt++;
            }

            for (int i = startAt + 1; i < value.Length; i++)
            {
                char c = value[i];

                char cPrev = value[i - 1];

                if (!char.IsDigit(cPrev) || char.IsDigit(c))
                {
                    continue;
                }

                return i;
            }
            
            return -1;
        }
    }
}
