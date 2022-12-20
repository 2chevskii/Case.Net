using Case.Net.Parsing;

namespace Case.Net.Common.Conventions;

public static class NamingConventions
{
    public static readonly INamingConvention CamelCase    = new CamelCaseNamingConvention();
    public static readonly INamingConvention PascalCase   = new PascalCaseNamingConvention();
    public static readonly INamingConvention SnakeCase    = new SnakeCaseNamingConvention();
    public static readonly INamingConvention KebabCase    = new KebabCaseNamingConvention();
    public static readonly INamingConvention TrainCase    = new TrainCaseNamingConvention();
    public static readonly INamingConvention ConstantCase = new ConstantCaseNamingConvention();

    public static INamingConvention Detect(
        ReadOnlySpan<char> input
    )
    {
        var parsed = Parse( input );

        return parsed.NamingConvention;
    }

    public static CasedString Parse(ReadOnlySpan<char> input)
    {
        throw new NotImplementedException();
    }
}
