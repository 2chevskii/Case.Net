namespace Case.NET.Parsing
{
    internal static class Utils
    {
        internal static bool Contains(char[] array, char c)
        {
            for (var i = array.Length - 1; i >= 0; i--)
            {
                if (array[i] == c)
                    return true;
            }

            return false;
        }
    }
}
