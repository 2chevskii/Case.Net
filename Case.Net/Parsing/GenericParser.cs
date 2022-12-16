using Case.Net.Common;
using Case.Net.Common.Conventions;
using Case.Net.Extensions;
using Case.Net.Parsing.CharFilters;
using Case.Net.Parsing.Prefixes;
using Case.Net.Parsing.Splitters;
using Case.Net.Parsing.Suffixes;

namespace Case.Net.Parsing;

public class GenericParser : IParser
{
    private readonly IPrefixParser[] _prefixParsers;
    private readonly ISuffixParser[] _suffixParsers;
    private readonly ISplitter[]     _splitters;

    public IReadOnlyCollection<IPrefixParser> PrefixParsers => _prefixParsers;
    public IReadOnlyCollection<ISuffixParser> SuffixParsers => _suffixParsers;
    public IReadOnlyCollection<ISplitter> Splitters => _splitters;

    public GenericParser(
        IEnumerable<ISplitter> splitters,
        IEnumerable<IPrefixParser> prefixParsers,
        IEnumerable<ISuffixParser> suffixParsers
    )
    {
        _splitters = splitters.ToArray();
        _prefixParsers = prefixParsers.ToArray();
        _suffixParsers = suffixParsers.ToArray();
    }

    private static void ClearSplitResults(Span<SplitResult> splitResults) =>
    splitResults.Fill( SplitResult.CreateEmpty() );

    public CasedString Parse(ReadOnlySpan<char> input)
    {
        if ( input.Length is 0 )
        {
            return CasedString.Empty;
        }

        (int prefixSize, IPrefixParser? prefixParser) = FindBiggestPrefix( input );
        var prefixSlice = input.Slice( 0, prefixSize );
        input = input.Slice( prefixSize );
        (int suffixSize, ISuffixParser? suffixParser) = FindBiggestSuffix( input );
        var suffixSlice = input[^suffixSize..];
        input = input[..^suffixSize];

        int               position     = 0;
        Span<SplitResult> splitResults = stackalloc SplitResult[_splitters.Length];

        List<Word>      words      = new ();
        List<Delimiter> delimiters = new ();

        while ( position < input.Length )
        {
            ClearSplitResults( splitResults );

            for ( var i = 0; i < _splitters.Length; i++ )
            {
                var splitter    = _splitters[i];
                var splitResult = splitter.Split( input.Slice( position ) );
                splitResults[i] = splitResult;
            }

            if ( splitResults.All( x => x.IsEmpty ) )
            {
                /*turn the whole remaining slice into word*/
                var word = new Word(
                    new Position( position, position + input.Length - 1 ),
                    input.ToString(),
                    null
                );

                words.Add( word );
                position = input.Length;
            }
            else
            {
                var minSr = splitResults.MinBy( x => x.TotalLength, out int srIndex );

                Position wordPos = new Position( position, position + minSr.WordLength - 1 );

                var word = new Word(
                    wordPos,
                    input.Slice( position, wordPos.Length ).ToString(),
                    _splitters[srIndex]
                );

                words.Add( word );

                if ( minSr.DelimiterLength is not 0 )
                {
                    int delimiterPosition = position + minSr.WordLength;
                    ReadOnlySpan<char> delimiterValueSlice = input.Slice(
                        delimiterPosition,
                        minSr.DelimiterLength
                    );

                    delimiters.Add(
                        new Delimiter( delimiterPosition, delimiterValueSlice.ToString() )
                    );
                }

                position += minSr.TotalLength;
            }
        }

        Prefix prefix;

        if ( prefixSize is 0 )
        {
            prefix = Prefix.Empty;
        }
        else
        {
            prefix = new Prefix( prefixParser!, prefixSlice.ToString() );
        }

        Suffix suffix;

        if ( suffixSize is 0 )
        {
            suffix = Suffix.Empty;
        }
        else
        {
            suffix = new Suffix( suffixParser!, suffixSlice.ToString() );
        }

        CasedString casedString = new CasedString(
            string.Empty,
            string.Empty,
            delimiters.Select( x => x.Value ).ToArray(),
            words.Select( x => x.Value ).ToArray(),
            NamingConventions.Detect( prefix, suffix, words, delimiters )
        );

        return casedString;
    }

    (int, IPrefixParser?) FindBiggestPrefix(ReadOnlySpan<char> input)
    {
        int            maxLength = 0;
        IPrefixParser? mParser   = null;

        for ( var i = 0; i < _prefixParsers.Length; i++ )
        {
            var parser = _prefixParsers[i];
            var length = parser.GetPrefixSize( input );

            if ( length > maxLength )
            {
                maxLength = length;
                mParser = parser;
            }
        }

        return (maxLength, mParser);
    }

    (int, ISuffixParser?) FindBiggestSuffix(ReadOnlySpan<char> input)
    {
        int            maxLength = 0;
        ISuffixParser? mParser   = null;

        for ( int i = 0; i < _suffixParsers.Length; i++ )
        {
            var parser = _suffixParsers[i];
            var length = parser.GetSuffixSize( input );

            if ( length > maxLength )
            {
                maxLength = length;
                mParser = parser;
            }
        }

        return (maxLength, mParser);
    }

    /*private void PushWord(
        ICollection<Word> words,
        ReadOnlySpan<char> slice,
        Span<(int, int)> splitPositions,
        ref int position
    )
    {
        List<(int, int, ISplitter)> meaningfulPositions = new ();

        for ( int i = 0; i < splitPositions.Length; i++ )
        {
            (int wordEnd, int nextPos) = splitPositions[i];

            if ( wordEnd is not -1 )
                meaningfulPositions.Add( (wordEnd, nextPos, _splitters[i]) );
        }

        int wordStart = position;
        int wordEndAbs,
            nextPosAbs;

        string value;

        ISplitter? splitter;

        if ( meaningfulPositions.Count is 0 )
        {
            wordEndAbs = position + slice.Length - 1;
            nextPosAbs = position + slice.Length;
            splitter = null;
            value = new string( slice );
        }
        else
        {
            (int minWordEnd, int minNextPos, ISplitter? splitter2) =
            meaningfulPositions.MinBy( x => x.Item1 );

            wordEndAbs = position + minWordEnd;
            nextPosAbs = position + minNextPos;
            splitter = splitter2;
            value = new string( slice[..(minWordEnd + 1)] );
        }

        var wordPosition = new Position( wordStart, wordEndAbs );
        var word         = new Word( wordPosition, value, splitter );
        position = nextPosAbs;
        words.Add( word );
    }

    private void ExecuteSplitters(ReadOnlySpan<char> input, Span<(int, int)> splitPositions)
    {
        for ( var i = 0; i < _splitters.Length; i++ )
        {
            ISplitter splitter = _splitters[i];

            (int wordEnd, int nextPos) = splitPositions[i];

            if ( splitter.TryFindSplitIndex( input, ref wordEnd, ref nextPos ) )
            {
                splitPositions[i] = (wordEnd, nextPos);
            }
        }
    }

    private void SkipFiltered(ReadOnlySpan<char> input, ref int position)
    {
        for ( ; position < input.Length; position++ )
        {
            char c       = input[position];
            bool skipped = false;

            for ( var i = 0; i < _charFilters.Length; i++ )
            {
                ICharFilter filter = _charFilters[i];

                if ( filter.ShouldIgnore( c ) )
                {
                    skipped = true;

                    break;
                }
            }

            if ( !skipped )
                break;
        }
    }*/

}
