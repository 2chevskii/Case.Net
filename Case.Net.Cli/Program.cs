using Case.Net.Common;
using Case.Net.Common.Conventions;

var convention = new CamelCaseNamingConvention();

var input = new CasedString(
    string.Empty,
    string.Empty,
    Array.Empty<string>(),
    new [] {"hello", "world"},
    new MixedNamingConvention()
);

CasedString output = convention.Convert(input);

Console.WriteLine(output);
