using Case.Net.Common;
using Case.Net.Emitters.Sanitizers;
using Case.Net.Extensions;

namespace Case.Net.Emitters.Words;

public class FirstUpperWordEmitter : IWordEmitter
{
    public readonly ISanitizer Sanitizer;

    public FirstUpperWordEmitter(ISanitizer sanitizer) { Sanitizer = sanitizer; }

    public bool EmitWord(CasedString source, int wordIndex, out ReadOnlySpan<char> wordBuffer)
    {
        var inputWord = source.WordAt( wordIndex );

        if ( inputWord.Length is 0 )
        {
            wordBuffer = ReadOnlySpan<char>.Empty;

            return false;
        }

        var sanitizedWord = Sanitizer.Sanitize( inputWord );
        var targetBuffer =
        new Span<char>( GC.AllocateUninitializedArray<char>( sanitizedWord.Length ) );

        wordBuffer = targetBuffer;

        targetBuffer[0] = char.ToUpperInvariant( sanitizedWord[0] );

        if ( targetBuffer.Length > 1 ) { sanitizedWord[1..].ToLowerInvariant( targetBuffer[1..] ); }

        return true;
    }
}
