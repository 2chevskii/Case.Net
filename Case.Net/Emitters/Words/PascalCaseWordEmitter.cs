using Case.Net.Common;
using Case.Net.Emitters.Sanitizers;

namespace Case.Net.Emitters.Words;

public class PascalCaseWordEmitter : IWordEmitter
{
    private readonly FirstUpperWordEmitter _firstUpperWordEmitter;

    public PascalCaseWordEmitter()
    {
        _firstUpperWordEmitter = new FirstUpperWordEmitter(new LetterOrDigitSanitizer());
    }

    public bool EmitWord(CasedString source, int wordIndex, out ReadOnlySpan<char> wordBuffer)
    {
        return _firstUpperWordEmitter.EmitWord( source, wordIndex, out wordBuffer );
    }
}
