using System;
using System.Collections.Generic;
using System.Linq;

using Case.NET.Parsing.Tokens;

namespace Case.NET
{
    public readonly struct ConvertedString // TODO: rename ConvertedString -> CasedString
    {
        public readonly string                      OriginalValue;
        public readonly string                      Value;
        public readonly IReadOnlyCollection<IToken> Tokens;
        public readonly ICaseConverter              Converter;

        public int OriginalLength => OriginalValue.Length;
        public int Length => Value.Length;

        public ConvertedString(string originalValue, string value, IReadOnlyCollection<IToken> tokens, ICaseConverter converter)
        {
            OriginalValue = originalValue ?? throw new ArgumentNullException(nameof(originalValue));
            Value = value ?? throw new ArgumentNullException(nameof(value));
            Tokens = tokens ?? throw new ArgumentNullException(nameof(tokens));
            Converter = converter ?? throw new ArgumentNullException(nameof(converter));
        }

        public static implicit operator string(ConvertedString convertedString)
        {
            return convertedString.Value;
        }

        public static explicit operator ConvertedString((string, ICaseConverter) tuple)
        {
            (string value, ICaseConverter converter) = tuple;

            return Create(value, converter);
        } 

        public static ConvertedString Create(string value, ICaseConverter converter)
        {
            if (converter == null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            return converter.ConvertCase(value);
        }

        public IEnumerable<WordToken> GetWords() => Tokens.OfType<WordToken>();
    }
}
