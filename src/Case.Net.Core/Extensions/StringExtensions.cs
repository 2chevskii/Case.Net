namespace Case.Net.Extensions
{
    public static class StringExtensions
    {
        public static bool IsEmpty(this string self) => self.Length is 0;

        public static bool EndsWith(this string self, char c)
        {
            return !self.IsEmpty() && self[self.Length - 1] == c;
        }

        public static bool StartsWith(this string self, char c)
        {
            return !self.IsEmpty() && self[0] == c;
        }
    }
}
