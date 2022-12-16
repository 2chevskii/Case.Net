using Case.Net.Common;

namespace Case.Net.Parsing.Splitters;

public readonly struct SplitResult
{
    public readonly int WordLength,
                        DelimiterLength;

    public int TotalLength => WordLength + DelimiterLength;

    public bool IsEmpty => WordLength is 0;

    public SplitResult(int wordLength) : this( wordLength, 0 ) { }

    public SplitResult(int wordLength, int delimiterLength)
    {
        WordLength = wordLength;
        DelimiterLength = delimiterLength;
    }

    public static SplitResult CreateEmpty() => new SplitResult( 0 );
}
