namespace Case.Net.Emit.Suffixes;

public interface ISuffixEmitter
{
    bool EmitSuffix(IReadOnlyList<string> words, out ReadOnlySpan<char> suffixBuffer);
}
