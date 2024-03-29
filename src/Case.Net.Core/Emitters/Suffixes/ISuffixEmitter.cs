﻿using System;
using System.Collections.Generic;

namespace Case.Net.Emitters.Suffixes
{
    public interface ISuffixEmitter
    {
        bool EmitSuffix(IReadOnlyList<string> words, out ReadOnlySpan<char> suffixBuffer);
    }
}
