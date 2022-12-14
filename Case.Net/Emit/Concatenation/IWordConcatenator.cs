using Case.Net.Common;

namespace Case.Net.Emit.Concatenation;

public interface IWordConcatenator
{
    ReadOnlySpan<char> GetConcatenation(CasedString source, int index);
}
