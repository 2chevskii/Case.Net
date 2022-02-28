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
        /// <summary>
        /// Default <see cref="CaseConverter"/> implementation to convert values to <c>camelCase</c>
        /// </summary>
        public static readonly CaseConverter CamelCase = new CaseConverter(
            Parsing.Parser.Universal,
            CamelCaseWordEmitter.Instance,
            EmptyWordConcatenator.Instance
        );
        /// <summary>
        /// Default <see cref="CaseConverter"/> implementation to convert values to <c>PascalCase</c>
        /// </summary>
        public static readonly CaseConverter PascalCase = new CaseConverter(
            Parsing.Parser.Universal,
            StateLessWordEmitter.FirstUpper,
            EmptyWordConcatenator.Instance
        );
        /// <summary>
        /// Default <see cref="CaseConverter"/> implementation to convert values to <c>snake_case</c>
        /// </summary>
        public static readonly CaseConverter SnakeCase = new CaseConverter(
            Parsing.Parser.Universal,
            StateLessWordEmitter.AllLower,
            SingleCharWordConcatenator.Underscore
        );
        /// <summary>
        /// Default <see cref="CaseConverter"/> implementation to convert values to <c>CONSTANT_CASE</c>
        /// </summary>
        public static readonly CaseConverter ConstantCase = new CaseConverter(
            Parsing.Parser.Universal,
            StateLessWordEmitter.AllUpper,
            SingleCharWordConcatenator.Underscore
        );
        /// <summary>
        /// Default <see cref="CaseConverter"/> implementation to convert values to <c>kebab-case</c>
        /// </summary>
        public static readonly CaseConverter KebabCase = new CaseConverter(
            Parsing.Parser.Universal,
            StateLessWordEmitter.AllLower,
            SingleCharWordConcatenator.Dash
        );
        /// <summary>
        /// Default <see cref="CaseConverter"/> implementation to convert values to <c>Train-Case</c>
        /// </summary>
        public static readonly CaseConverter TrainCase = new CaseConverter(
            Parsing.Parser.Universal,
            StateLessWordEmitter.FirstUpper,
            SingleCharWordConcatenator.Dash
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

            // This looks weird, but be can save CPU cycles
            // getting rid of if(i != tokens.Count - 1)
            // while checking if we should append word concatenator
            int i = 0;
            int l = tokens.Count - 1;
            for (; i < l; i++)
            {
                stringBuilder.Append(WordEmitter.Emit(tokens, i));

                stringBuilder.Append(WordConcatenator.GetConcatenation(tokens, i));
            }

            stringBuilder.Append(WordEmitter.Emit(tokens, l));

            if (PrefixEmitter != null)
            {
                stringBuilder.Insert(0, PrefixEmitter.GetPrefix(tokens, stringBuilder));
            }

            if (SuffixEmitter != null)
            {
                stringBuilder.Append(SuffixEmitter.GetSuffix(tokens, stringBuilder));
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
