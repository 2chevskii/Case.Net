namespace Case.NET.Parsing.WordSplitting
{
    public class SingleCharWordSplitter : IWordSplitter
    {
        public static readonly SingleCharWordSplitter Dash       = new SingleCharWordSplitter('-');
        public static readonly SingleCharWordSplitter Underscore = new SingleCharWordSplitter('_');
        public static readonly SingleCharWordSplitter Dot        = new SingleCharWordSplitter('.');
        public static readonly SingleCharWordSplitter Whitespace = new SingleCharWordSplitter(' ');
        public static readonly SingleCharWordSplitter Tab        = new SingleCharWordSplitter('\t');
        public static readonly SingleCharWordSplitter LineFeed   = new SingleCharWordSplitter('\n');
        public static readonly SingleCharWordSplitter CaretReturn =
            new SingleCharWordSplitter('\r');
        public static readonly SingleCharWordSplitter Backslash = new SingleCharWordSplitter('\\');
        public static readonly SingleCharWordSplitter Forwardslash =
            new SingleCharWordSplitter('/');
        public static readonly SingleCharWordSplitter Comma = new SingleCharWordSplitter(',');

        public readonly char SplitChar;

        public SingleCharWordSplitter(char splitChar)
        {
            SplitChar = splitChar;
        }

        public int TryFindSplitIndex(string value, int startAt, out bool skipIndexChar)
        {
            for (int i = startAt; i < value.Length; i++)
            {
                char c = value[i];

                if (c != SplitChar)
                {
                    continue;
                }

                skipIndexChar = true;

                return i;
            }

            skipIndexChar = false;

            return -1;
        }
    }
}
