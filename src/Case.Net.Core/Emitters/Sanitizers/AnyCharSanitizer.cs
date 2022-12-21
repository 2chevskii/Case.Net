using System;

namespace Case.Net.Emitters.Sanitizers
{
    public class AnyCharSanitizer : ISanitizer
    {
        public ReadOnlySpan<char> Sanitize(ReadOnlySpan<char> input)
        {
            return input;
        }
    }
}
