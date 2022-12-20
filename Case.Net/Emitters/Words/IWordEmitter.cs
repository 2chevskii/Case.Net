using Case.Net.Common;

namespace Case.Net.Emitters.Words;

public interface IWordEmitter
{
    bool EmitWord(CasedString source, int wordIndex, out ReadOnlySpan<char> wordBuffer);
}
