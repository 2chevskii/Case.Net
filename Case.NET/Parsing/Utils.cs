using System.Collections.Generic;

namespace Case.NET.Parsing
{
    internal static class Utils
    {
        internal static bool Contains(IReadOnlyList<char> array, char c)
        {
            for (var i = array.Count - 1; i >= 0; i--)
            {
                if (array[i] == c)
                    return true;
            }

            return false;
        }

        internal static void FillArray<T>(T[] array, T value) where T : struct
        {
            for (var i = 0; i < array.Length; i++)
            {
                array[i] = value;
            }
        }
    }
}
