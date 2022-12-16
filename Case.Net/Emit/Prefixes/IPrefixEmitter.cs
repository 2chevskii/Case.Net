namespace Case.Net.Emit.Prefixes;

public interface IPrefixEmitter
{
    ReadOnlySpan<char> GetPrefix(ReadOnlySpan<char> value);
}
