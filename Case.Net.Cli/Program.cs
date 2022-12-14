using Case.Net;
using Case.Net.Parsing;
using Case.Net.Parsing.CharFilters;
using Case.Net.Parsing.Splitters;

var parser = new GenericParser( new ICharFilter[0], new[]{new CamelCaseSplitter()} );

var words = parser.Parse("camelCase".AsSpan());

Console.WriteLine(words);
