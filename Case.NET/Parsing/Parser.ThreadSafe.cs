#if NETSTANDARD2_1_OR_GREATER
using System;
using System.Collections.Generic;

using Case.NET.Parsing.Filters;
using Case.NET.Parsing.Tokens;
using Case.NET.Parsing.WordSplitting;
#endif

namespace Case.NET.Parsing
{
#if NETSTANDARD2_1_OR_GREATER
    /// <summary>
    /// Basic parser implementation. Only produces <see cref="WordToken"/> at the moment
    /// <para>
    /// Thread safe. Uses stackalloc'ed arrays
    /// inside <see cref="Parse"/> method instead of member fields
    /// </para>
    /// </summary> 
#endif
    public partial class Parser
    {
#if NETSTANDARD2_1_OR_GREATER
        public virtual IList<IToken> Parse(in string value/*, bool returnSourceIfNoMatches*/)
        {
            Span<int> splitIndexArray = stackalloc int[wordSplitters.Length];
            Span<bool> skipCharsArray = stackalloc bool[wordSplitters.Length];

            splitIndexArray.Fill(-1);

            List<IToken> tokens = new List<IToken>();

            int startAt = 0;

            while (startAt < value.Length)
            {
                for (int i = 0; i < charFilters.Length; i++)
                {
                    ICharFilter filter = charFilters[i];

                    while (startAt < value.Length && filter.ShouldSkip(value, startAt))
                        startAt++;
                }

                if (startAt == value.Length)
                    break;

                for (int i = 0; i < wordSplitters.Length; i++)
                {
                    IWordSplitter splitter = wordSplitters[i];

                    int index = splitter.TryFindSplitIndex(value, startAt, out bool skip);

                    splitIndexArray[i] = index;
                    skipCharsArray[i] = skip;
                }

                int minIndex = -1;
                bool doSkip = false;

                for (int i = 0; i < splitIndexArray.Length; i++) // FIXME: start from second element
                {
                    int index = splitIndexArray[i];

                    if (index != -1 && (minIndex == -1 || index < minIndex))
                    {
                        minIndex = index;
                        doSkip = skipCharsArray[i];
                    }
                }

                int length = (minIndex != -1 ? minIndex : value.Length) - startAt;

                string substring = value.Substring(startAt, length);

                WordToken word = new WordToken(startAt, substring);

                tokens.Add(word);

                if (minIndex != -1)
                {
                    startAt = doSkip ? minIndex + 1 : minIndex;
                }
                else
                {
                    startAt = value.Length;
                }
            }

            //if (tokens.Count == 0 && returnSourceIfNoMatches)
            //{
            //    tokens.Add(new WordToken(0, value));
            //}

            return tokens;
        }
#endif
    }
}
