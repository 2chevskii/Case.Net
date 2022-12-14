using Case.Net.Common;

namespace Case.Net.Emit.Suffixes;

public interface ISuffixEmitter
{
    ReadOnlySpan<char> GetSuffix(CasedString source);
}
