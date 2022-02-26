using System;

namespace Case.NET.Parsing.WordSplitting
{
    public class CamelCaseWordSplitter : IWordSplitter
    {
#pragma warning disable CS0618
        public static readonly CamelCaseWordSplitter Instance = new CamelCaseWordSplitter();
#pragma warning restore CS0618

        [Obsolete("Use static instance field instead")]
        public CamelCaseWordSplitter() { }

        public int TryFindSplitIndex(string value, int startAt, out bool skipIndexChar)
        {
            skipIndexChar = false;

            for (int i = startAt + 1; i < value.Length; i++)
            {
                char c = value[i],
                     cPrev = value[i - 1];

                if (!char.IsLetter(c) ||
                    !char.IsLetter(cPrev) ||
                    !char.IsLower(cPrev) ||
                    !char.IsUpper(c))
                {
                    continue;
                }

                return i;
            }

            return -1;
        }
    }
}
