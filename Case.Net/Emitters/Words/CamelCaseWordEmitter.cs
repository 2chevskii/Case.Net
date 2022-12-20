using Case.Net.Common;
using Case.Net.Emitters.Sanitizers;

namespace Case.Net.Emitters.Words;

public class CamelCaseWordEmitter : IWordEmitter
{
    private readonly FirstUpperWordEmitter _firstUpperWordEmitter;
    private readonly AllLowerWordEmitter   _allLowerWordEmitter;

    public CamelCaseWordEmitter()
    {
        var sanitizer = new LetterOrDigitSanitizer();

        _firstUpperWordEmitter = new ( sanitizer );
        _allLowerWordEmitter   = new ( sanitizer );
    }

    public bool EmitWord(CasedString source, int wordIndex, out ReadOnlySpan<char> wordBuffer)
    {
        if ( wordIndex is 0 )
        {
            return _allLowerWordEmitter.EmitWord( source, wordIndex, out wordBuffer );
        }

        return _firstUpperWordEmitter.EmitWord( source, wordIndex, out wordBuffer );
    }
}
