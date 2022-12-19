using Case.Net.Common;

namespace Case.Net.Emit.Words;

public interface IWordEmitter
{
    string EmitWord(CasedString source, int wordIndex);
}
