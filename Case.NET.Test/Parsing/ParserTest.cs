using System.Collections.Generic;
using System.Linq;

using Case.NET.Parsing;
using Case.NET.Parsing.Tokens;
using Case.NET.Parsing.WordSplitting;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Case.NET.Test.Parsing
{
    [TestClass]
    public class ParserTest
    {
        private static readonly TestString[] CamelCasedStrings = {
            new TestString("helloWorld", new WordToken(0, "hello"), new WordToken(5, "World")),
            new TestString(
                "fooBarBaz",
                new WordToken(0, "foo"),
                new WordToken(3, "Bar"),
                new WordToken(6, "Baz")
            ),
            new TestString("PascalCase", new WordToken(0, "Pascal"), new WordToken(6, "Case"))
        };
        private static readonly TestString[] KebabCasedStrings = {
            new TestString("hello-world", new WordToken(0, "hello"), new WordToken(6, "world")),
            new TestString(
                "foo-bar-baz",
                new WordToken(0, "foo"),
                new WordToken(4, "bar"),
                new WordToken(8, "baz")
            ),
            new TestString(
                "Train-Cased-String",
                new WordToken(0, "Train"),
                new WordToken(6, "Cased"),
                new WordToken(12, "String")
            )
        };
        private static readonly TestString[] SnakeCasedStrings = {
            new TestString("hello_world", new WordToken(0, "hello"), new WordToken(6, "world"))
        };

        public static IEnumerable<object[]> GetTestParseDataValidCasing()
        {
            yield return new object[] {
                new Parser(CamelCaseWordSplitter.Instance),
                CamelCasedStrings
            };

            yield return new object[] {
                new Parser(SingleCharWordSplitter.Dash),
                KebabCasedStrings
            };

            yield return new object[] {
                new Parser(SingleCharWordSplitter.Underscore),
                SnakeCasedStrings
            };
        }

        public static IEnumerable<object[]> GetTestParseDataNoMatch()
        {
            yield return new object[] {
                new Parser(CamelCaseWordSplitter.Instance),
                new[] {
                    new TestString("hello, world", new WordToken(0, "hello, world")),
                    new TestString("hello_world", new WordToken(0, "hello_world"))
                }
            };
        }

        [DataTestMethod]
        [DynamicData(nameof(GetTestParseDataValidCasing), DynamicDataSourceType.Method)]
        [DynamicData(nameof(GetTestParseDataNoMatch), DynamicDataSourceType.Method)]
        public void TestParse(IParser parser, TestString[] data)
        {
            for (var i = 0; i < data.Length; i++)
            {
                TestString currentData = data[i];

                string value = currentData.Value;
                WordToken[] expected = currentData.ExpectedTokens;

                var tokens = parser.Parse(value);

                CollectionAssert.AreEqual(expected, tokens.Cast<WordToken>().ToArray());
            }
        }

        public readonly struct TestString
        {
            public readonly string      Value;
            public readonly WordToken[] ExpectedTokens;

            public TestString(string value, params WordToken[] expectedTokens)
            {
                Value = value;
                ExpectedTokens = expectedTokens;
            }
        }
    }
}
