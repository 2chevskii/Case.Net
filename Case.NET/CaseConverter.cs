using System;
using System.Collections.Generic;
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
        public static readonly CaseConverter CamelCase = new CaseConverter(
            Parsing.Parser.Universal,
            new CamelCaseWordEmitter(),
            new EmptyWordConcatenator()
        );
        public static readonly CaseConverter PascalCase = new CaseConverter(
            Parsing.Parser.Universal,
            StateLessWordEmitter.FirstUpper,
            new EmptyWordConcatenator()
        );

        public IParser Parser { get; }
        public IWordEmitter WordEmitter { get; }
        public IWordConcatenator WordConcatenator { get; }
        public IPrefixEmitter PrefixEmitter { get; }
        public ISuffixEmitter SuffixEmitter { get; }

        public CaseConverter(
            IParser parser,
            IWordEmitter wordEmitter,
            IWordConcatenator wordConcatenator,
            IPrefixEmitter prefixEmitter = null,
            ISuffixEmitter suffixEmitter = null
        )
        {
            WordEmitter = wordEmitter ?? throw new ArgumentNullException(nameof(wordEmitter));
            Parser = parser ?? throw new ArgumentNullException(nameof(parser));
            WordConcatenator = wordConcatenator ??
                               throw new ArgumentNullException(nameof(wordConcatenator));

            PrefixEmitter = prefixEmitter;
            SuffixEmitter = suffixEmitter;
        }

        public CasedString ConvertCase(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }

            return ConvertCase(Parser.Parse(value), value);
        }

        public CasedString ConvertCase(IReadOnlyList<WordToken> tokens, string originalValue)
        {
            if (tokens.Count < 2)
            {
                // skip sb allocation

                string casedValue = WordEmitter.Emit(tokens, 0);

                return new CasedString(
                    originalValue,
                    (PrefixEmitter != null
                         ? PrefixEmitter.GetPrefix(tokens, casedValue)
                         : string.Empty) +
                    casedValue +
                    (SuffixEmitter != null
                         ? SuffixEmitter.GetSuffix(tokens, casedValue)
                         : string.Empty),
                    tokens,
                    this
                );
            }

            StringBuilder stringBuilder = new StringBuilder();

            for (var i = 0; i < tokens.Count; i++)
            {
                stringBuilder.Append(WordEmitter.Emit(tokens, i));
            }

            if (PrefixEmitter != null)
            {
                stringBuilder.Insert(0, PrefixEmitter.GetPrefix(tokens, stringBuilder));
            }

            if (SuffixEmitter != null)
            {
                stringBuilder.Insert(0, SuffixEmitter.GetSuffix(tokens, stringBuilder));
            }

            return new CasedString(
                originalValue,
                stringBuilder.ToString(),
                tokens,
                this
            );
        }
    }
}
