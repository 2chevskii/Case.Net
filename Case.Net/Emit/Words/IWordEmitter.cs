using Case.Net.Common;

namespace Case.Net.Emit.Words;

public interface IWordEmitter
{
    ReadOnlySpan<char> EmitWord(CasedString source, int wordIndex);
}
