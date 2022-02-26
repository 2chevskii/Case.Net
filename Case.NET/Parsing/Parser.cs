using System.Collections.Generic;
using System.Linq;

using Case.NET.Parsing.Tokens;
using Case.NET.Parsing.WordSplitting;

namespace Case.NET.Parsing
{
    public class Parser : IParser
    {
        public static readonly IParser UniversalParser = new Parser(
            new IWordSplitter[] {
                new LineEndWordSplitter(),
                new DashWordSplitter(),
                new UnderscoreWordSplitter(),
                new SpaceWordSplitter()
            }
        );
        protected readonly IWordSplitter[] wordSplitters;

        public IReadOnlyCollection<IWordSplitter> WordSplitters => wordSplitters;

        protected Parser(IEnumerable<IWordSplitter> wordSplitters)
        {
            this.wordSplitters = wordSplitters.ToArray();
        }

        public virtual ICollection<IToken> Parse(string value, bool includeSplitTokens)
        {
            List<IToken> tokens = new List<IToken>();
            int lastIndex = 0;

            for (var i = 0; i < value.Length; i++)
            {
                int bufferedChars = i - lastIndex;

                for (var j = 0; j < wordSplitters.Length; j++)
                {
                    var splitter = wordSplitters[j];

                    bool splitted = splitter.CanSplit(
                        value,
                        i,
                        bufferedChars,
                        out bool skip,
                        out SplitToken splitToken
                    );

                    if (splitted)
                    {
                        string val = value.Substring(lastIndex, i - lastIndex);
                        tokens.Add(new WordToken(lastIndex, val));

                        if (includeSplitTokens)
                        {
                            tokens.Add(splitToken);
                        }

                        lastIndex = i + 1;
                    }

                    if (skip)
                    {
                        if (includeSplitTokens)
                        {
                            tokens.Add(splitToken);
                        }

                        lastIndex = i + 1;
                        break;
                    }
                }
            }

            return tokens;
        }
    }
}
