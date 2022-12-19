namespace Case.Net.Emit.Suffixes;

public interface ISuffixEmitter
{
    string EmitSuffix(IReadOnlyList<string> words);
}
