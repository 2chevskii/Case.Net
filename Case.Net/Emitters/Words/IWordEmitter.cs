using Case.Net.Common;
using Case.Net.Common.Entities;

namespace Case.Net.Emitters.Words;

public interface IWordEmitter
{
    bool EmitWord(CasedString source, int wordIndex, out ReadOnlySpan<char> wordBuffer);
}
