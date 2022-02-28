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
        protected bool           changed;
        protected ICaseConverter builtSubject;

        protected IParser           parser;
        protected IWordEmitter      emitter;
        protected IWordConcatenator concatenator;
        protected IPrefixEmitter    prefixEmitter;
        protected ISuffixEmitter    suffixEmitter;

        public ICaseConverter Build()
        {
            throw new NotImplementedException();

            if (!changed)
            {
                return builtSubject;
            }
        }

        public CaseConverterBuilder WithParser(IParser parser)
        {
            throw new NotImplementedException();
        }

        public CaseConverterBuilder WithParser(Action<ParserBuilder> buildAction)
        {
            throw new NotImplementedException();
        }

        public CaseConverterBuilder WithWordEmitter(IWordEmitter wordEmitter)
        {
            throw new NotImplementedException();
        }

        public CaseConverterBuilder WithWordEmitter(Action<WordEmitterBuilder> buildAction)
        {
            throw new NotImplementedException();
        }

        public CaseConverterBuilder WithWordConcatenator(IWordConcatenator wordConcatenator)
        {
            throw new NotImplementedException();
        }

        public CaseConverterBuilder WithWordConcatenator(Action<IWordConcatenator> buildAction)
        {
            throw new NotImplementedException();
        }

        public CaseConverterBuilder WithPrefixEmitter(IPrefixEmitter prefixEmitter)
        {
            throw new NotImplementedException();
        }

        public CaseConverterBuilder WithPrefixEmitter(Action<PrefixEmitterBuilder> buildAction)
        {
            throw new NotImplementedException();
        }

        public CaseConverterBuilder WithSuffixEmitter(ISuffixEmitter suffixEmitter)
        {
            throw new NotImplementedException();
        }

        public CaseConverterBuilder WithSuffixEmitter(Action<SuffixEmitterBuilder> buildAction)
        {
            throw new NotImplementedException();
        }

        public CasedString ConvertCase(string value) => Build().ConvertCase(value);

        public CasedString ConvertCase(IReadOnlyList<WordToken> tokens, string originalValue) =>
            Build().ConvertCase(tokens, originalValue);
    }
}
