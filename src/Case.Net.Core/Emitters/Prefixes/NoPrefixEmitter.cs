using System;
using System.Collections.Generic;

namespace Case.Net.Emitters.Prefixes
{
    public class NoPrefixEmitter : IPrefixEmitter
    {

        public bool EmitPrefix(IReadOnlyList<string> words, out ReadOnlySpan<char> prefixBuffer)
        {
            prefixBuffer = ReadOnlySpan<char>.Empty;
            return false;
        }
    }
}
