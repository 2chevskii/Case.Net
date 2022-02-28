using System;

namespace Case.NET.Parsing.WordSplitting
{
    // TODO: Remove code duplication here and in CamelCaseWordSplitter, TrailingNumberWordSplitter
    // TODO: by implementing BacktrackWordSplitter -> .ctor(Func<char, bool> validateFunc, int backtrackLength)
    public class UpperLowerCaseWordSplitter : IWordSplitter
    {
        public static readonly UpperLowerCaseWordSplitter Instance =
#pragma warning disable CS0618
            new UpperLowerCaseWordSplitter();
#pragma warning restore CS0618

        [Obsolete("Use static instance field instead")]
        public UpperLowerCaseWordSplitter() { }

        public int TryFindSplitIndex(string value, int startAt, out bool skipIndexChar)
        {
            skipIndexChar = false;

            for (int i = startAt + 2; i < value.Length; i++)
            {
                char c = value[i];
                char cPrev = value[i - 1];
                char cPrevPrev = value[i - 2];

                if (!char.IsLetter(cPrevPrev) ||
                    !char.IsLetter(cPrev) ||
                    !char.IsLetter(c) ||
                    !char.IsUpper(cPrevPrev) ||
                    !char.IsUpper(cPrev) ||
                    !char.IsLower(c))
                {
                    continue;
                }

                return i;
            }

            return -1;
        }
    }
}
