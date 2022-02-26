namespace Case.NET.Parsing.WordSplitting
{
    public class CaretReturnWordSplitter : SingleCharWordSplitter
    {

        public override char SplitChar => '\r';
    }
}
