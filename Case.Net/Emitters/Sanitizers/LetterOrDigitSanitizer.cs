using Case.Net.Extensions;

namespace Case.Net.Emit.Sanitizers;

public class LetterOrDigitSanitizer : ISanitizer
{

    public ReadOnlySpan<char> Sanitize(ReadOnlySpan<char> input)
    {
        Span<char> buffer = stackalloc char[input.Length];

        int bufferCount = 0;

        for ( int i = 0; i < input.Length; i++ )
        {
            char current = input[i];

            if ( current.IsLetterOrDigit() ) { buffer[bufferCount++] = current; }
        }

        if ( bufferCount is 0 ) { return ReadOnlySpan<char>.Empty; }

        Span<char> result = new char[bufferCount];

        buffer.CopyTo( result );

        return result;
    }
}
