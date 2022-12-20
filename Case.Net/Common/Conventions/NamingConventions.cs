namespace Case.Net.Common.Conventions;

public static class NamingConventions
{
    public static readonly INamingConvention CamelCase  = new CamelCaseNamingConvention();
    public static readonly INamingConvention PascalCase = new PascalCaseNamingConvention();
    public static readonly INamingConvention SnakeCase  = new SnakeCaseNamingConvention();
    public static readonly INamingConvention KebabCase  = new KebabCaseNamingConvention();

    public static INamingConvention Detect(
        ReadOnlySpan<char> input
    )
    {
        throw new NotImplementedException();
    }

    public static CasedString Parse(ReadOnlySpan<char> input)
    {
        throw new NotImplementedException();
    }
}
