namespace Case.Net.Parsing.CharFilters;

public class SingleCharFilter : ICharFilter
{
    public char FilterChar { get; }

    public SingleCharFilter(char filterChar)
    {
        FilterChar = filterChar;
    }

    public bool ShouldIgnore(char @char)
    {
        return @char.Equals( FilterChar );
    }
}
