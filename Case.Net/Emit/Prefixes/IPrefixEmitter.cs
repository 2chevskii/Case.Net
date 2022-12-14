using Case.Net.Common;

namespace Case.Net.Emit.Prefixes;

public interface IPrefixEmitter
{
    ReadOnlySpan<char> GetPrefix(CasedString source);
}
