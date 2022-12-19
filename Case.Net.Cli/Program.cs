using Case.Net.Common.Conventions;

using Sprache;

namespace Case.Net.Cli;

public class Program
{
    static Parser<string> dashParser = Parse.Char( '-' ).Select(x => x.ToString());
    static Parser<string> firstWordParser = from firstLetter in Parse.Lower
                                            from restString in Parse.Lower.Many()
                                                                    .Or( Parse.Digit.Many() )
                                                                    .Text()
                                            select string.Concat( firstLetter, restString );
    static Parser<string> otherWordsParser = Parse.Lower.AtLeastOnce().Or( Parse.Digit.AtLeastOnce() ).Text();
    static Parser<IEnumerable<string>> kebabParser = from first in firstWordParser
                                                     from rest in dashParser
                                                                  .Then( dash => otherWordsParser )
                                                                  .Many()
                                                     select new[] {first}.Concat( rest );
    public static void Main(string[] args)
    {
        /*var camelCase  = new CamelCaseNamingConvention();
        var pascalCase = new PascalCaseNamingConvention();
        var snakeCase  = new SnakeCaseNamingConvention();

        var ccstr = camelCase.Parse( "camelCasedString" );
        var pcstr = pascalCase.Parse( "PascalCasedString" );
        var scstr = snakeCase.Parse( "snake_cased_string" );

        Console.WriteLine(ccstr.ToString());
        Console.WriteLine(pcstr.ToString());
        Console.WriteLine(scstr.ToString());*/


        var strings = kebabParser.Parse( "kebab-cased-string" );
        Console.WriteLine( strings );
    }
}
