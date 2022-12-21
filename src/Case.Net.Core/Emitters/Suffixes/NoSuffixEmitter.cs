using System;
using System.Collections.Generic;

namespace Case.Net.Emitters.Suffixes
{
    public class NoSuffixEmitter : ISuffixEmitter
    {
        public bool EmitSuffix(IReadOnlyList<string> words, out ReadOnlySpan<char> suffixBuffer)
        {
            suffixBuffer = ReadOnlySpan<char>.Empty;

            return false;
        }
    }
}
