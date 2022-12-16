using Case.Net.Common;

namespace Case.Net.Extensions;

public static class EnumerableExtensions
{
    public static int CharCount(this List<string> self)
    {
        int count = 0;

        for ( var i = 0; i < self.Count; i++ )
        {
            count += self[i].Length;
        }

        return count;
    }

    public static int CharCount(this List<Word> self)
    {
        int count = 0;

        for ( int i = 0; i < self.Count; i++ )
        {
            count += self[i].Value.Length;
        }

        return count;
    }
}
