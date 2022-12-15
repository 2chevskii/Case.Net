namespace Case.Net.Emit.Suffixes;

public interface ISuffixEmitter
{
    ReadOnlySpan<char> GetSuffix(ReadOnlySpan<char> value);
}
