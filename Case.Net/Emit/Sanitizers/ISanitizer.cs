namespace Case.Net.Emit.Sanitizers;

public interface ISanitizer
{
    ReadOnlySpan<char> Sanitize(ReadOnlySpan<char> input);
}
