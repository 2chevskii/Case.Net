using Case.Net.Common;

namespace Case.Net.Emit.Words;

public interface IWordEmitter
{
    Word EmitWord(CasedString source, int wordIndex);
}
