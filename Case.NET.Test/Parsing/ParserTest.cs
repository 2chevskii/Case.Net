using System;
using System.Collections.Generic;

using Case.NET.Parsing;
using Case.NET.Parsing.Tokens;
using Case.NET.Parsing.WordSplitting;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json;

namespace Case.NET.Test.Parsing
{
    [TestClass]
    public class ParserTest
    {
        [DataTestMethod]
        [DynamicData(nameof(GetTestParseData), DynamicDataSourceType.Method)]
        public void TestParse(IParser parser, string data, WordToken[] expected)
        {
            var tokens = parser.Parse(data, false);

            for (var i = 0; i < tokens.Count; i++)
            {
                var token = (WordToken) tokens[i];

                var expectedToken = expected[i];

                Assert.AreEqual(expectedToken, token);
            }
        }

        public static IEnumerable<object[]> GetTestParseData()
        {
            yield return new object[] {
                new Parser(CamelCaseWordSplitter.Instance),
                "hello, world",
                new[] {new WordToken(0, "hello, world")}
            };

            yield return new object[] {
                new Parser(CamelCaseWordSplitter.Instance),
                "helloWorld",
                new[] {
                    new WordToken(0, "hello"),
                    new WordToken(5, "World")
                }
            };

            yield return new object[] {
                new Parser(CamelCaseWordSplitter.Instance),
                "AnotherHelloWorld",
                new[] {
                    new WordToken(0, "Another"),
                    new WordToken(7, "Hello"),
                    new WordToken(12, "World")
                }
            };

        }
    }
}
