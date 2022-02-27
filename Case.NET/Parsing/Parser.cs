using System;
using System.Collections.Generic;
using System.Linq;

using Case.NET.Parsing.Filters;
using Case.NET.Parsing.Tokens;
using Case.NET.Parsing.WordSplitting;

namespace Case.NET.Parsing
{
#if NETSTANDARD2_0
    /// <summary>
    /// Basic parser implementation. Only produces <see cref="WordToken"/> at the moment
    /// <para>
    /// NOT thread safe. Use single instance for each thread
    /// </para>
    /// </summary> 
#endif
    public partial class Parser : IParser
    {
        private static readonly ICharFilter[] EmptyCharFilterArray = Array.Empty<ICharFilter>();

        protected readonly IWordSplitter[] wordSplitters;
        protected readonly ICharFilter[]   charFilters;

#if NETSTANDARD2_0
        protected readonly int[]
            splitIndexArray; // those two arrays are used for memoizing values of the current pass
        protected readonly bool[] skipCharsArray; // therefore - this Parser impl is not thread-safe
#endif

#if NETSTANDARD2_1_OR_GREATER
        public static readonly Parser Universal = new Parser(
            new IWordSplitter[] {
                CamelCaseWordSplitter.Instance,
                UpperLowerCaseWordSplitter.Instance,
                SingleCharWordSplitter.Dash,
                SingleCharWordSplitter.Underscore,
                SingleCharWordSplitter.Whitespace,
            },
            new ICharFilter[] {CharFilter.CommonDelimiters}
        );
#endif

#if NETSTANDARD2_0
        public static Parser Universal => new Parser(
            new IWordSplitter[] {
                CamelCaseWordSplitter.Instance,
                UpperLowerCaseWordSplitter.Instance,
                SingleCharWordSplitter.Dash,
                SingleCharWordSplitter.Underscore,
                SingleCharWordSplitter.Whitespace,
            },
            new ICharFilter[] {CharFilter.CommonDelimiters}
        );
#endif

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
#if NETSTANDARD2_0
            splitIndexArray = new int[this.wordSplitters.Length];
            skipCharsArray = new bool[this.wordSplitters.Length];
            Utils.FillArray(splitIndexArray, -1);
#endif
        }

#if NETSTANDARD2_0
        public virtual IReadOnlyList<WordToken> Parse(string value)
        {
            List<WordToken> tokens = new List<WordToken>();

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

                int minIndex = splitIndexArray[0];
                bool doSkip = skipCharsArray[0];

                for (int i = 1; i < splitIndexArray.Length; i++)
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
            
            return tokens;
        }
#endif
    }
}
