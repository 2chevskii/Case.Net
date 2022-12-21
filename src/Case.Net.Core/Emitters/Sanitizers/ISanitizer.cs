using System;

namespace Case.Net.Emitters.Sanitizers
{
    public interface ISanitizer
    {
        ReadOnlySpan<char> Sanitize(ReadOnlySpan<char> input);
    }
}
