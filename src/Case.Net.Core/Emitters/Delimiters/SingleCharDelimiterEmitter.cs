﻿using System;
using System.Collections.Generic;

using Case.Net.Extensions;

namespace Case.Net.Emitters.Delimiters
{
    public class SingleCharDelimiterEmitter : IDelimiterEmitter
    {
        public char Delimiter { get; }
        public bool CheckWordEndStart { get; }

        public SingleCharDelimiterEmitter(char delimiter, bool checkWordEndStart = true)
        {
            Delimiter         = delimiter;
            CheckWordEndStart = checkWordEndStart;
        }

        public bool EmitDelimiter(
            IReadOnlyList<string> words,
            int index,
            out ReadOnlySpan<char> delimiterBuffer
        )
        {
            string current = words[index];

            if ( CheckWordEndStart && current.EndsWith( Delimiter ) )
            {
                delimiterBuffer = ReadOnlySpan<char>.Empty;

                return false;
            }

            string next = words[index + 1];

            if ( CheckWordEndStart && next.StartsWith( Delimiter ) )
            {
                delimiterBuffer = ReadOnlySpan<char>.Empty;

                return false;
            }

            Span<char> buffer;

#if NET5_0_OR_GREATER
            buffer = new Span<char>( GC.AllocateUninitializedArray<char>( 1 ) );
#else
            buffer = new Span<char>( new char[1] );
#endif

            buffer[0] = Delimiter;

            delimiterBuffer = buffer;

            return true;
        }
    }
}
