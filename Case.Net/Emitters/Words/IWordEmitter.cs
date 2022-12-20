using Case.Net.Common;

namespace Case.Net.Emit.Words;

public interface IWordEmitter
{
    bool EmitWord(CasedString source, int wordIndex, out ReadOnlySpan<char> wordBuffer);
}
