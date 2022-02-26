using System.Collections.Generic;
using System.Linq;

namespace Case.NET.Parsing.Filters
{
    public class CharFilter : ICharFilter
    {
        public static readonly CharFilter CommonDelimiters = new CharFilter(
            new[] {
                '_',
                '.',
                '-',
                ' ',
                '\t',
                '\n',
                '\r',
                ','
            }
        );

        public IReadOnlyList<char> SkipChars { get; }

        public CharFilter(char skipChar)
        {
            SkipChars = new[] {skipChar};
        }

        public CharFilter(IEnumerable<char> skipChars)
        {
            SkipChars = skipChars.ToArray();
        }

        public CharFilter(IEnumerable<int> skipChars)
        {
            // ReSharper disable once SuspiciousTypeConversion.Global
            SkipChars = skipChars.Cast<char>().ToArray();
        }

        public virtual bool ShouldSkip(string value, int index) =>
            Utils.Contains(SkipChars, value[index]);
    }
}
