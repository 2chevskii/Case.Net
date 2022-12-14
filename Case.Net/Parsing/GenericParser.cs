using Case.Net.Common;
using Case.Net.Parsing.CharFilters;
using Case.Net.Parsing.Splitters;

namespace Case.Net.Parsing;

public class GenericParser : IParser
{
    private readonly ICharFilter[] _charFilters;
    private readonly ISplitter[]   _splitters;

    public IReadOnlyCollection<ICharFilter> CharFilters => _charFilters;
    public IReadOnlyCollection<ISplitter> Splitters => _splitters;

    public GenericParser(IEnumerable<ICharFilter> charFilters, IEnumerable<ISplitter> splitters)
    {
        _charFilters = charFilters.ToArray();
        _splitters   = splitters.ToArray();
    }

    private static void ClearSplitPositions(Span<(int, int)> splitPositions) =>
    splitPositions.Fill( (-1, -1) );

    public IReadOnlyCollection<Word> Parse(ReadOnlySpan<char> input)
    {
        if ( input.Length is 0 )
            return Array.Empty<Word>();

        int              position       = 0;
        Span<(int, int)> splitPositions = stackalloc (int, int)[_splitters.Length];
        List<Word>       words          = new ();

        while ( position < input.Length )
        {
            ReadOnlySpan<char> slice = input[position..];

            SkipFiltered( slice, ref position );

            ReadOnlySpan<char> slice2 = input[position..];

            ClearSplitPositions( splitPositions );

            ExecuteSplitters( slice2, splitPositions );

            PushWord( words, slice2, splitPositions, ref position );
        }

        return words;
    }

    private void PushWord(
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
            splitter   = null;
            value      = new string( slice );
        }
        else
        {
            (int minWordEnd, int minNextPos, ISplitter? splitter2) =
            meaningfulPositions.MinBy( x => x.Item1 );

            wordEndAbs = position + minWordEnd;
            nextPosAbs = position + minNextPos;
            splitter   = splitter2;
            value      = new string( slice[..(minWordEnd + 1)] );
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
    }

}
