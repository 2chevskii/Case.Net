using System;
using System.Collections.Generic;

using Case.NET.Parsing.Tokens;

namespace Case.NET
{
    public readonly struct CasedString
    {
        public readonly string                   OriginalValue;
        public readonly string                   Value;
        public readonly IReadOnlyList<WordToken> Tokens;
        public readonly ICaseConverter           Converter;

        public int OriginalLength => OriginalValue.Length;
        public int Length => Value.Length;

        public CasedString(
            string originalValue,
            string value,
            IReadOnlyList<WordToken> tokens,
            ICaseConverter converter
        )
        {
            OriginalValue = originalValue ?? throw new ArgumentNullException(nameof(originalValue));
            Value = value ?? throw new ArgumentNullException(nameof(value));
            Tokens = tokens ?? throw new ArgumentNullException(nameof(tokens));
            Converter = converter ?? throw new ArgumentNullException(nameof(converter));
        }

        public static implicit operator string(CasedString casedString)
        {
            return casedString.Value;
        }

        public static explicit operator CasedString((string, ICaseConverter) tuple)
        {
            (string value, ICaseConverter converter) = tuple;

            return Create(value, converter);
        }

        public static CasedString Create(string value, ICaseConverter converter)
        {
            if (converter == null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            return converter.ConvertCase(value);
        }
    }
}
