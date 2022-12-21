using System;
using System.Collections.Generic;

namespace Case.Net.Emitters.Delimiters
{
    public interface IDelimiterEmitter
    {
        bool EmitDelimiter(IReadOnlyList<string> words, int index, out ReadOnlySpan<char> delimiterBuffer);
    }
}
