using System;
using System.Collections.Generic;

namespace Case.Net.Emitters.Delimiters
{
    public class NoDelimiterEmitter : IDelimiterEmitter
    {

        public bool EmitDelimiter(
            IReadOnlyList<string> words,
            int index,
            out ReadOnlySpan<char> delimiterBuffer
        )
        {
            delimiterBuffer = ReadOnlySpan<char>.Empty;
            return false;
        }
    }
}
