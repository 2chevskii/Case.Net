using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Case.Net.Parsing.Prefix;

public interface IPrefixParser
{
    bool TryParsePrefix(ReadOnlySpan<char> input, out ReadOnlySpan<char> prefix);
}

public class CharPrefixParser : IPrefixParser
{
    public readonly char PrefixChar;
    public readonly int  Limit;

    public CharPrefixParser(char prefixChar, int limit = 1)
    {
        if ( limit < 1 )
            throw new ArgumentOutOfRangeException( nameof( limit ) );

        PrefixChar = prefixChar;
        Limit      = limit;
    }

    public bool TryParsePrefix(ReadOnlySpan<char> input, out ReadOnlySpan<char> prefix)
    {
        int i = 0;

        while ( i < input.Length )
        {
            char c = input[i];

            if ( c != PrefixChar )
            {
                break;
            }

            i++;
        }

        if ( i is 0 )
        {
            prefix=ReadOnlySpan<char>.Empty;
            return false;
        }

        prefix = input.Slice( 0, i + 1 );

        return true;
    }
}

public class CharSetPrefixParser : IPrefixParser
{
    public readonly IReadOnlyList<char> PrefixChars;
    public readonly int                 Limit;

    public CharSetPrefixParser(IEnumerable<char> prefixChars, int limit=1)
    {
        if ( limit < 1 )
            throw new ArgumentOutOfRangeException( nameof( limit ) );

        PrefixChars = prefixChars.ToArray();
        Limit       = limit;
    }

    public bool TryParsePrefix(ReadOnlySpan<char> input, out ReadOnlySpan<char> prefix)
    {
        int i = 0;

        while ( i<input.Length )
        {
            char c = input[i];

            bool f=false;

            for ( int j = 0; j < PrefixChars.Count; j++ )
            {
                char pc = PrefixChars[j];

                if ( c == pc )
                {
                    f = true;

                    break;
                }
            }

            if ( !f )
            {
                break;
            }

            i++;
        }

        if ( i is 0 )
        {
            prefix = ReadOnlySpan<char>.Empty;

            return false;
        }

        prefix = input.Slice( 0, i + 1 );

        return true;
    }
}

public class StringPrefixParser : IPrefixParser
{
    private readonly string PrefixString;
    private readonly int    Limit;

    public StringPrefixParser(string prefixString, int limit=1)
    {
        PrefixString = prefixString;
        Limit   = limit;
    }

    public bool TryParsePrefix(ReadOnlySpan<char> input, out ReadOnlySpan<char> prefix)
    {
        for ( int i = 0; (i + PrefixString.Length - 1) < input.Length; i++ )
        {

        }
    }
}
