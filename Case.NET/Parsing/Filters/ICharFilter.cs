namespace Case.NET.Parsing.Filters
{
    public interface ICharFilter
    {
        bool ShouldSkip(string value, int index);
    }
}
