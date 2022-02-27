using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Case.NET.Emit.Concat;
using Case.NET.Emit.PostProcessing;
using Case.NET.Emit.Words;
using Case.NET.Parsing;
using Case.NET.Parsing.Tokens;

namespace Case.NET
{
    public class CaseConverter : ICaseConverter
    {
        public IParser Parser => parser;
        public IWordEmitter WordEmitter => wordEmitter;
        public IWordConcatenator WordConcatenator => wordConcatenator;
        public IPrefixEmitter PrefixEmitter => throw new NotImplementedException();
        public ISuffixEmitter SuffixEmitter => throw new NotImplementedException();

        protected readonly IParser           parser;
        protected readonly IWordEmitter      wordEmitter;
        protected readonly IWordConcatenator wordConcatenator;
        protected readonly IPrefixEmitter    prefixEmitter;
        protected readonly ISuffixEmitter    suffixEmitter;

        public CaseConverter(
            IParser parser,
            IWordEmitter wordEmitter,
            IWordConcatenator wordConcatenator
        )
        {
            this.wordEmitter = wordEmitter ?? throw new ArgumentNullException(nameof(wordEmitter));
            this.parser = parser ?? throw new ArgumentNullException(nameof(parser));
            this.wordConcatenator = wordConcatenator ??
                                    throw new ArgumentNullException(nameof(wordConcatenator));
        }

        public ConvertedString ConvertCase(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }

            var tokens = Parser.Parse(value);

            if (!(tokens is IList<WordToken> wTokens)) // FIXME: need to deal with this type hell
            {
                throw new NotImplementedException();
            }
            else
            {
                return ConvertCase(value, wTokens);
            }

            //return ConvertCase(tokens);
        }

        public ConvertedString ConvertCase(string originalValue, IList<WordToken> tokens)
        {
            if (tokens.Count < 2)
            {
                // skip sb allocation

                string word = wordEmitter.Emit(tokens, 0);
                string prefix = prefixEmitter != null
                                    ? prefixEmitter.GetPrefix(tokens, word)
                                    : string.Empty;

                string suffix = suffixEmitter != null
                                    ? suffixEmitter.GetSuffix(tokens, word)
                                    : string.Empty;

                return new ConvertedString(
                    originalValue,
                    prefix + word + suffix,
                    (IReadOnlyCollection<IToken>)tokens, // FIXME
                    this
                );
            }
            else
            {
                StringBuilder builder = new StringBuilder();

                for (var i = 0; i < tokens.Count; i++)
                {
                    builder.Append(wordEmitter.Emit(tokens, i));
                }

                string word = builder.ToString();
                string prefix = prefixEmitter != null
                                    ? prefixEmitter.GetPrefix(tokens, word)
                                    : string.Empty;

                string suffix = suffixEmitter != null
                                    ? suffixEmitter.GetSuffix(tokens, word)
                                    : string.Empty;

                return new ConvertedString(
                    originalValue,
                    prefix + word + suffix,
                    (IReadOnlyCollection<IToken>) tokens,
                    this
                );
            }
        }

        ConvertedString ICaseConverter.ConvertCase(IList<IToken> tokens) =>
            throw new NotImplementedException();
        //ConvertCase(tokens.Cast<WordToken>().ToArray());
    }
}
