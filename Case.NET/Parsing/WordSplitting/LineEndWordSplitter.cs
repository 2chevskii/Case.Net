namespace Case.NET.Parsing.WordSplitting
{
    public class LineEndWordSplitter : VariableCharWordSplitter
    {
        public override char[] SplitChars => new[] {
            '\n',
            '\r'
        };
    }
}
