using System;
using System.Collections.Generic;

using Case.NET.Parsing;
using Case.NET.Parsing.Filters;
using Case.NET.Parsing.WordSplitting;

namespace Case.NET.Extensions.FluentAPI
{
    public class ParserBuilder : IBuilder<IParser>
    {
        protected readonly ICollection<IWordSplitter> wordSplitters;
        protected readonly ICollection<ICharFilter>   charFilters;

        public IReadOnlyCollection<IWordSplitter> WordSplitters =>
            (IReadOnlyCollection<IWordSplitter>) wordSplitters;
        public IReadOnlyCollection<ICharFilter> CharFilters =>
            (IReadOnlyCollection<ICharFilter>) charFilters;

        public ParserBuilder()
        {
            wordSplitters = new HashSet<IWordSplitter>();
            charFilters = new HashSet<ICharFilter>();
        }

        /// <summary>
        /// Creates <see cref="IParser"/> with selected <see cref="WordSplitters"/> and <see cref="CharFilters"/><br/>
        /// <remarks>If no <see cref="WordSplitters"/> are present, <see cref="SingleCharWordSplitter.Whitespace"/> will be used</remarks>
        /// </summary>
        /// <returns>Built parser</returns>
        public IParser Build()
        {
            return new Parser(
                wordSplitters.Count != 0
                    ? wordSplitters
                    : new[] {SingleCharWordSplitter.Whitespace},
                charFilters
            );
        }

        public ParserBuilder WithWordSplitter(IWordSplitter wordSplitter)
        {
            wordSplitters.Add(wordSplitter ?? throw new ArgumentNullException());

            return this;
        }

        public ParserBuilder WithWordSplitters(IEnumerable<IWordSplitter> wordSplitters)
        {
            if (wordSplitters == null)
            {
                throw new ArgumentNullException();
            }

            foreach (IWordSplitter wordSplitter in wordSplitters)
            {
                this.wordSplitters.Add(wordSplitter);
            }

            return this;
        }

        public ParserBuilder WithoutWordSplitter(IWordSplitter wordSplitter)
        {
            wordSplitters.Remove(wordSplitter ?? throw new ArgumentNullException());

            return this;
        }

        public ParserBuilder WithoutWordSplitters(IEnumerable<IWordSplitter> wordSplitters)
        {
            if (wordSplitters == null)
            {
                throw new ArgumentNullException();
            }

            return this;
        }

        public ParserBuilder WithoutWordSplitters()
        {
            wordSplitters.Clear();

            return this;
        }

        public ParserBuilder WithCharFilter(ICharFilter charFilter)
        {
            charFilters.Add(charFilter ?? throw new ArgumentNullException());

            return this;
        }

        public ParserBuilder WithCharFilters(IEnumerable<ICharFilter> charFilters)
        {
            if (charFilters == null)
            {
                throw new ArgumentNullException();
            }

            foreach (ICharFilter charFilter in charFilters)
            {
                this.charFilters.Add(charFilter);
            }

            return this;
        }

        public ParserBuilder WithoutCharFilter(ICharFilter charFilter)
        {
            charFilters.Add(charFilter ?? throw new ArgumentNullException());

            return this;
        }

        public ParserBuilder WithoutCharFilters(IEnumerable<ICharFilter> charFilters)
        {
            if (charFilters == null)
            {
                throw new ArgumentNullException();
            }

            foreach (ICharFilter charFilter in charFilters)
            {
                this.charFilters.Remove(charFilter);
            }

            return this;
        }

        public ParserBuilder WithoutCharFilters()
        {
            charFilters.Clear();

            return this;
        }
    }
}
