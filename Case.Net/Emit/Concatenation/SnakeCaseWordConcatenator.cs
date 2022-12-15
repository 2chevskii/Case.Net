using Case.Net.Common;

namespace Case.Net.Emit.Concatenation;

public class SnakeCaseWordConcatenator : IWordConcatenator
{
    private const string  UNDERSCORE = "_";

    public string GetConcatenation(ReadOnlySpan<char> current, ReadOnlySpan<char> next)
    {
        return UNDERSCORE;
    }
}
