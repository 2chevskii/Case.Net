using Case.Net.Common.Conventions;
using Case.Net.Emit.Delimiters;
using Case.Net.Emit.Prefixes;
using Case.Net.Emit.Sanitizers;
using Case.Net.Emit.Suffixes;
using Case.Net.Emit.Words;
using Case.Net.Extensions;

namespace Case.Net.Cli;

public class Program
{
    static class Strings
    {
        public const string CamelCase  = "camel0CasedString";
        public const string PascalCase = "PascalCased0String";
        public const string SnakeCase  = "snake_cased_string";
        public const string KebabCase  = "kebab-cased-string";
    }

    public static void Main()
    {
        var camelCaseConvention = new CamelCaseNamingConvention();

        var camelCasedString = camelCaseConvention.Parse( Strings.CamelCase );

        Console.WriteLine( camelCasedString.ToDebugString() );

        var pascalCaseNamingConvention = new PascalCaseNamingConvention();

        var pascalCasedString = pascalCaseNamingConvention.Parse( Strings.PascalCase );

        Console.WriteLine( pascalCasedString.ToDebugString() );

        var snakeCaseNamingConvention = new SnakeCaseNamingConvention();

        var snakeCasedString = snakeCaseNamingConvention.Parse(Strings.SnakeCase);

        Console.WriteLine( snakeCasedString.ToDebugString() );

        var kebabCaseNamingConvention = new KebabCaseNamingConvention();

        var kebabCasedString = kebabCaseNamingConvention.Parse( Strings.KebabCase );

        Console.WriteLine( kebabCasedString.ToDebugString() );

        Console.WriteLine( camelCaseConvention.Convert( kebabCasedString ).ToDebugString() );

        Console.WriteLine( pascalCaseNamingConvention.Convert( snakeCasedString ).ToDebugString() );

        Console.WriteLine(kebabCaseNamingConvention.Convert(pascalCasedString).ToDebugString());

        Console.WriteLine( snakeCaseNamingConvention.Convert( kebabCasedString ).ToDebugString() );

        var trainCaseNamingConvention = NamingConventions.TrainCase;

        Console.WriteLine( trainCaseNamingConvention.Convert( kebabCasedString ).ToDebugString() );

        Console.WriteLine(trainCaseNamingConvention.Parse("Train-Cased-String-Here").ToDebugString());
    }
}
