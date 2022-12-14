using Case.Net.Common;

namespace Case.Net.Extensions;

public static class WordExtensions
{
    public static bool IsLastWord(this Word word)
    {
        return word.Splitter is null;
    }
}
