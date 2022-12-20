using Case.Net.Emit.Delimiters;
using Case.Net.Emit.Prefixes;
using Case.Net.Emit.Suffixes;
using Case.Net.Emit.Words;
using Case.Net.Parsing;

namespace Case.Net.Common.Conventions;

public static class NamingConventions
{
    public static readonly INamingConvention CamelCase  = new CamelCaseNamingConvention();
    public static readonly INamingConvention PascalCase = new PascalCaseNamingConvention();
    public static readonly INamingConvention SnakeCase  = new SnakeCaseNamingConvention();
    public static readonly INamingConvention KebabCase  = new KebabCaseNamingConvention();
    public static readonly INamingConvention TrainCase = new GenericNamingConvention(
        "Train-Case",
        new PascalCaseWordEmitter(),
        new NoPrefixEmitter(),
        new NoSuffixEmitter(),
        new SingleCharDelimiterEmitter( '-', false ),
        new DelimiterFirstUpperParser( '-' ),
        false
    );

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
