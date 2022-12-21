using System;

namespace Case.Net.Parsing
{
    public readonly struct WordPosition
    {
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
}
