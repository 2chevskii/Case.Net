using System;

namespace Case.NET.Parsing.Tokens
{
    [Obsolete("Currently has no use")]
    public readonly struct SplitToken : IToken
    {
        public int Index { get; }
        public int Length { get; }
        public int EndIndex => Length != 0 ? Index + Length - 1 : Index;
        public string Value { get; }
        public bool HasValue { get; }
        public bool IsMeaningful { get; }

        public SplitToken(int index, string value) : this(
            index,
            value,
            value.Length,
            value.Length != 0,
            value.Length != 0
        ) { }

        public SplitToken(
            int index,
            string value,
            int length,
            bool hasValue,
            bool isMeaningful
        )
        {
            Index = index;
            Value = value;
            Length = length;
            HasValue = hasValue;
            IsMeaningful = isMeaningful;
        }
    }
}
