using System;
using System.Collections.Generic;
using System.Linq;

using Case.NET.Parsing.Filters;
using Case.NET.Parsing.Tokens;
using Case.NET.Parsing.WordSplitting;

namespace Case.NET.Parsing
{
    public class Parser : IParser
    {
        private static readonly ICharFilter[] EmptyCharFilterArray = Array.Empty<ICharFilter>();

        protected readonly IWordSplitter[] wordSplitters;
        protected readonly ICharFilter[] charFilters;
        protected readonly int[]
            splitIndexArray; // those two arrays are used for memoizing values of the current pass
        protected readonly bool[] skipCharsArray; // therefore - this Parser impl is not thread-safe

        public IReadOnlyCollection<IWordSplitter> WordSplitters => wordSplitters;
        public IReadOnlyCollection<ICharFilter> CharFilters => charFilters;

        public Parser(params IWordSplitter[] wordSplitters) : this(
            (IEnumerable<IWordSplitter>) wordSplitters
        ) { }

        public Parser(IEnumerable<IWordSplitter> wordSplitters) : this(wordSplitters, null) { }

        public Parser(
            IEnumerable<IWordSplitter> wordSplitters,
            IEnumerable<ICharFilter> charFilters
        )
        {
            this.wordSplitters = wordSplitters?.ToArray() ??
                                 throw new ArgumentNullException(nameof(wordSplitters));

            if (this.wordSplitters.Length == 0)
            {
                throw new ArgumentException(
                    "Word splitters array cannot be empty",
                    nameof(wordSplitters)
                );
            }

            this.charFilters = charFilters?.ToArray() ?? EmptyCharFilterArray;
            splitIndexArray = new int[this.wordSplitters.Length];
            skipCharsArray = new bool[this.wordSplitters.Length];
            Utils.FillArray(splitIndexArray, -1);
        }

        public virtual IList<IToken> Parse(string value, bool returnSourceIfNoMatches)
        {
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

            if (tokens.Count == 0 && returnSourceIfNoMatches)
            {
                tokens.Add(new WordToken(0, value));
            }

            return tokens;
        }
    }
}
