namespace Case.Net.Emit.Prefixes;

public interface IPrefixEmitter
{
    string EmitPrefix(IReadOnlyList<string> words);
}
