using Case.Net.Common;

namespace Case.Net.Emit.Concatenation;

public class KebabCaseWordConcatenator : IWordConcatenator
{
    private static char DASH = '-';

    public unsafe ReadOnlySpan<char> GetConcatenation(CasedString source, int index)
    {
        fixed (char* pDash = &DASH)
        {
            return new ReadOnlySpan<char>( pDash, 1 );
        }
    }
}
