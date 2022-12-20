using Case.Net.Common;
using Case.Net.Common.Conventions;

namespace Case.Net.Extensions;

public static class CasedStringExtensions
{
    public static bool IsKnownConvention(this CasedString self)
    {
        return self.NamingConvention is not MixedNamingConvention;
    }

    public static bool IsEmpty(this CasedString self)
    {
        return self.WordCount() is 0;
    }

    public static int WordCount(this CasedString self)
    {
        return self.Words.Count;
    }

    public static bool HasPrefix(this CasedString self)
    {
        return !self.Prefix.IsEmpty();
    }

    public static bool HasSuffix(this CasedString self)
    {
        return !self.Suffix.IsEmpty();
    }

    public static bool IsSingleWord(this CasedString self)
    {
        return self.WordCount() is 1;
    }

    public static string WordAt(this CasedString self, int index) => self.Words[index].ToString();

    public static string ToDebugString(this CasedString self)
    {
        const string format = @"
Words: [{0}]
Delimiters: [{1}]
Prefix: {2}
Suffix: {3}
";

        string debugView = string.Format(
            format,
            string.Join( ", ", self.Words.Select( word => $"\"{word}\"" ) ),
            string.Join( ", ", self.Delimiters.Select( delimiter => $"\"{delimiter}\"" ) ),
            self.Prefix,
            self.Suffix
        );

        return debugView;
    }

    public static bool HasDelimiterFor(this CasedString self, int wordIndex)
    {
        for ( var i = 0; i < self.Delimiters.Count; i++ )
        {
            Delimiter delimiter = self.Delimiters[i];

            if ( wordIndex == delimiter.PreviousWordIndex )
                return true;
        }

        return false;
    }

    public static Delimiter GetDelimiterFor(this CasedString self, int wordIndex)
    {
        for ( var i = 0; i < self.Delimiters.Count; i++ )
        {
            Delimiter delimiter = self.Delimiters[i];

            if ( wordIndex == delimiter.PreviousWordIndex )
                return delimiter;
        }

        return Delimiter.Empty;
    }
}
