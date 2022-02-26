namespace Case.NET.Parsing.Tokens
{
    public readonly struct WordToken : IToken
    {
        public int Index { get; }
        public int Length { get; }
        public int EndIndex => Index + Length - 1;
        public string Value { get; }

        public WordToken(int index, string value) : this(index, value, value.Length) { }

        public WordToken(int index, string value, int length)
        {
            Index = index;
            Length = length;
            Value = value;
        }
    }
}
