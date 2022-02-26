namespace Case.NET.Parsing.Tokens
{
    public interface IToken
    {
        int Index { get; }
        int Length { get; }
        int EndIndex { get; }
        string Value { get; }
    }
}
