namespace Case.NET.Parsing.WordSplitting
{
    public class SpaceWordSplitter : VariableCharWordSplitter
    {
        public override char[] SplitChars => new[] {
            ' ',
            '\t'
        };
    }
}
