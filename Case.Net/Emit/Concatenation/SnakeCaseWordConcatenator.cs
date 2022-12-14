using Case.Net.Common;

namespace Case.Net.Emit.Concatenation;

public class SnakeCaseWordConcatenator : IWordConcatenator
{
    private static char UNDERSCORE = '_';

    public unsafe ReadOnlySpan<char> GetConcatenation(CasedString source, int index)
    {
        fixed (char* pUnderscore = &UNDERSCORE)
        {
            return new ReadOnlySpan<char>( pUnderscore, 1 );
        }
    }
}