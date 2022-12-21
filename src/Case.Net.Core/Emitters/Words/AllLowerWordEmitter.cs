using System;

using Case.Net.Common;
using Case.Net.Common.Entities;
using Case.Net.Emitters.Sanitizers;
using Case.Net.Extensions;

namespace Case.Net.Emitters.Words
{
    public class AllLowerWordEmitter : IWordEmitter
    {
        public readonly ISanitizer Sanitizer;

        public AllLowerWordEmitter(ISanitizer sanitizer) { Sanitizer = sanitizer; }

        public bool EmitWord(CasedString source, int wordIndex, out ReadOnlySpan<char> wordBuffer)
        {
            string inputWord = source.WordAt( wordIndex );

            if ( inputWord.Length is 0 )
            {
                wordBuffer = ReadOnlySpan<char>.Empty;

                return false;
            }

            ReadOnlySpan<char> sanitizedWord = Sanitizer.Sanitize( inputWord .AsSpan());
            Span<char>     targetBuffer;

#if NET5_0_OR_GREATER
            targetBuffer =
 new Span<char>( GC.AllocateUninitializedArray<char>( sanitizedWord.Length ) );
#else
            targetBuffer = new Span<char>( new char[sanitizedWord.Length] );
#endif

            wordBuffer = targetBuffer;

            sanitizedWord.ToLowerInvariant( targetBuffer );

            return true;
        }
    }
}
