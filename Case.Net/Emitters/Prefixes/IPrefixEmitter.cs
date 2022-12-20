namespace Case.Net.Emit.Prefixes;

public interface IPrefixEmitter
{
    bool EmitPrefix(IReadOnlyList<string> words, out ReadOnlySpan<char> prefixBuffer);
}
