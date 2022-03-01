using System;
using System.Collections.Generic;

using Case.NET.Emit.Concat;
using Case.NET.Emit.PostProcessing;
using Case.NET.Emit.Words;
using Case.NET.Parsing;
using Case.NET.Parsing.Tokens;

namespace Case.NET.Extensions.FluentAPI
{
    public class CaseConverterBuilder : IBuilder<ICaseConverter>
    {
        protected IParser           parser;
        protected IWordEmitter      wordEmitter;
        protected IWordConcatenator wordConcatenator;
        protected IPrefixEmitter    prefixEmitter;
        protected ISuffixEmitter    suffixEmitter;

        public IParser Parser => parser;
        public IWordEmitter WordEmitter => wordEmitter;
        public IWordConcatenator WordConcatenator => wordConcatenator;
        public IPrefixEmitter PrefixEmitter => prefixEmitter;
        public ISuffixEmitter SuffixEmitter => suffixEmitter;

        /// <summary>
        /// Creates <see cref="ICaseConverter"/> with selected options<br/>
        /// <remarks>If no <see cref="Parser"/> is present, <see cref="Parsing.Parser.Universal"/> will be used<br/></remarks>
        /// <remarks>If no <see cref="WordEmitter"/> is present, <see cref="CamelCaseWordEmitter"/> will be used<br/></remarks>
        /// <remarks>If no <see cref="WordConcatenator"/> is present, <see cref="EmptyWordConcatenator"/> will be used</remarks>
        /// </summary>
        /// <returns>Built converter</returns>
        public ICaseConverter Build()
        {
            IParser useParser = parser ?? Parsing.Parser.Universal;
            IWordEmitter useWordEmitter = wordEmitter ?? CamelCaseWordEmitter.Instance;
            IWordConcatenator useWordConcatenator =
                wordConcatenator ?? EmptyWordConcatenator.Instance;

            CaseConverter converter = new CaseConverter(
                useParser,
                useWordEmitter,
                useWordConcatenator,
                prefixEmitter,
                suffixEmitter
            );

            return converter;
        }

        public CaseConverterBuilder WithParser(IParser parser)
        {
            this.parser = parser ?? throw new ArgumentNullException();

            return this;
        }

        public CaseConverterBuilder WithParser(Action<ParserBuilder> buildAction)
        {
            if (buildAction == null)
            {
                throw new ArgumentNullException();
            }

            var builder = new ParserBuilder();

            buildAction(builder);

            parser = builder.Build();

            return this;
        }

        public CaseConverterBuilder WithWordEmitter(IWordEmitter wordEmitter)
        {
            this.wordEmitter = wordEmitter ?? throw new ArgumentNullException();

            return this;
        }

        public CaseConverterBuilder WithWordConcatenator(IWordConcatenator wordConcatenator)
        {
            this.wordConcatenator = wordConcatenator ?? throw new ArgumentNullException();

            return this;
        }
        
        public CaseConverterBuilder WithPrefixEmitter(IPrefixEmitter prefixEmitter)
        {
            this.prefixEmitter = prefixEmitter ?? throw new ArgumentNullException();

            return this;
        }

        public CaseConverterBuilder WithoutPrefixEmitter()
        {
            prefixEmitter = null;

            return this;
        }

        public CaseConverterBuilder WithSuffixEmitter(ISuffixEmitter suffixEmitter)
        {
            this.suffixEmitter = suffixEmitter;

            return this;
        }

        public CaseConverterBuilder WithoutSuffixEmitter()
        {
            suffixEmitter = null;

            return this;
        }

        public CasedString ConvertCase(string value) => Build().ConvertCase(value);

        public CasedString ConvertCase(IReadOnlyList<WordToken> tokens, string originalValue) =>
            Build().ConvertCase(tokens, originalValue);
    }
}
