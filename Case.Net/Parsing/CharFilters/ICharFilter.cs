namespace Case.Net.Parsing.CharFilters;

public interface ICharFilter
{
    bool ShouldIgnore(char @char);
}
