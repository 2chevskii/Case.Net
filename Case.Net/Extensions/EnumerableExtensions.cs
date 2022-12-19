namespace Case.Net.Extensions;

public static class EnumerableExtensions
{
    public static int CharCount(this IEnumerable<string> self)
    {
        return self.Sum( str => str.Length );
    }
}
