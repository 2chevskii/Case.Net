using System.Collections.Generic;

namespace Case.NET.Parsing
{
    internal static class Utils
    {
        internal static bool Contains(IReadOnlyList<char> list, char c)
        {
            for (var i = 0; i < list.Count; i++)
            {
                if (list[i] == c)
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
