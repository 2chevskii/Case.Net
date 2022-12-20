using Case.Net.Common;
using Case.Net.Emitters.Sanitizers;
using Case.Net.Extensions;

namespace Case.Net.Emitters.Words;

public class AllLowerWordEmitter : IWordEmitter
{
    public readonly ISanitizer Sanitizer;

    public AllLowerWordEmitter(ISanitizer sanitizer) { Sanitizer = sanitizer; }

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

        sanitizedWord.ToLowerInvariant( targetBuffer );

        return true;
    }
}