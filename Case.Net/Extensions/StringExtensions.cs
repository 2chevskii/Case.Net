namespace Case.Net.Extensions;

public static class StringExtensions
{
    public static bool IsEmpty(this string self) => self.Length is 0;
}
