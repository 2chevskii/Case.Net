namespace Case.Net.Parsing;

public interface IWordParser
{
    bool TryParse(ReadOnlySpan<char> input, out IReadOnlyList<WordPosition> words);
}

public readonly struct WordPosition
{
    /*
     * Parsing string rtl,
     * "longCamelCasedStringHere":
     * 1. "long":
     *      Word end = 3
     *      DelimiterEnd = 3
     * ...
     */

    public readonly int WordEnd;
    public readonly int DelimiterEnd;

    public bool HasDelimiter => DelimiterEnd > WordEnd;

    public WordPosition(int wordEnd, int delimiterEnd)
    {
        if ( wordEnd < 0 )
            throw new ArgumentOutOfRangeException( nameof( wordEnd ) );

        if ( delimiterEnd < 0 )
            throw new ArgumentOutOfRangeException( nameof( delimiterEnd ) );

        if ( wordEnd > delimiterEnd )
            throw new ArgumentException( null, nameof( wordEnd ) );

        WordEnd      = wordEnd;
        DelimiterEnd = delimiterEnd;
    }

    public void Deconstruct(out int wordEnd, out int delimiterEnd)
    {
        wordEnd      = WordEnd;
        delimiterEnd = DelimiterEnd;
    }
}
