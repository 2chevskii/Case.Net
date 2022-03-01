using System;
using System.Collections.Generic;

using Case.NET.Parsing;
using Case.NET.Parsing.Filters;
using Case.NET.Parsing.Tokens;
using Case.NET.Parsing.WordSplitting;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Case.NET.Test.Parsing
{
    [TestClass]
    public class ParserTest
    {
        private const int DATA_ARR_CAPACITY = 4;

        public static IEnumerable<object[]> CamelCaseData { get; set; }
        public static IEnumerable<object[]> PascalCaseData { get; set; }
        public static IEnumerable<object[]> SnakeCaseData { get; set; }
        public static IEnumerable<object[]> ConstantCaseData { get; set; }
        public static IEnumerable<object[]> KebabCaseData { get; set; }
        public static IEnumerable<object[]> TrainCaseData { get; set; }

        [ClassInitialize]
        public static void Initialize(TestContext _)
        {
            // Some weird shit going on here
            // iterators were returning default WordToken values
            // until i made it the way it is now
            // needs some digging i guess, but i'm 2 mad for mstest atm
            CamelCaseData = GetCamelCaseData();
            PascalCaseData = GetPascalCaseData();
            SnakeCaseData = GetSnakeCaseData();
            ConstantCaseData = GetConstantCaseData();
            KebabCaseData = GetKebabCaseData();
            TrainCaseData = GetTrainCaseData();
        }

        [DataTestMethod]
        [DynamicData(nameof(GetCtorTestData), DynamicDataSourceType.Method)]
        public void TestParserCtor(
            IWordSplitter[] splitters,
            ICharFilter[] charFilters,
            bool expectThrow
        )
        {
            Action ctorAction = () => new Parser(splitters, charFilters);

            if (expectThrow)
            {
                ctorAction.Should().Throw<ArgumentException>();
            }
            else
            {
                ctorAction.Should().NotThrow();
            }
        }

        [DataTestMethod]
        [DynamicData(nameof(CamelCaseData))]
        [DynamicData(nameof(PascalCaseData))]
        [DynamicData(nameof(SnakeCaseData))]
        [DynamicData(nameof(ConstantCaseData))]
        [DynamicData(nameof(KebabCaseData))]
        [DynamicData(nameof(TrainCaseData))]
        public void TestUniversalParser(string data, WordToken[] expectedTokens)
        {
            var parser = Parser.Universal;

            var actualTokens = parser.Parse(data);

            for (var i = 0; i < actualTokens.Count; i++)
            {
                var aTok = actualTokens[i];
                var eTok = expectedTokens[i];

                aTok.Should().Be(eTok);
            }
        }

        public static IEnumerable<object[]> GetCtorTestData()
        {
            yield return new object[] {
                new[] {CamelCaseWordSplitter.Instance},
                new[] {CharFilter.CommonDelimiters},
                false
            };

            yield return new object[] {
                new[] {UpperLowerCaseWordSplitter.Instance},
                null,
                false
            };

            yield return new object[] {
                new[] {SingleCharWordSplitter.Backslash},
                new ICharFilter[0],
                false
            };

            yield return new object[] {
                null,
                null,
                true
            };

            yield return new object[] {
                new IWordSplitter[0],
                new[] {CharFilter.CommonDelimiters},
                true
            };
        }

        public static IEnumerable<object[]> GetCamelCaseData()
        {
            var enumerator = GetCaseTestData(Strings.CamelCase, Tokens.CamelCase);

            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }
        }

        public static IEnumerable<object[]> GetPascalCaseData()
        {
            var enumerator = GetCaseTestData(Strings.PascalCase, Tokens.PascalCase);

            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }
        }

        public static IEnumerable<object[]> GetSnakeCaseData()
        {
            var enumerator = GetCaseTestData(Strings.SnakeCase, Tokens.SnakeCase);

            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }
        }

        public static IEnumerable<object[]> GetConstantCaseData()
        {
            var enumerator = GetCaseTestData(Strings.ConstantCase, Tokens.ConstantCase);

            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }
        }

        public static IEnumerable<object[]> GetKebabCaseData()
        {
            var enumerator = GetCaseTestData(Strings.KebabCase, Tokens.KebabCase);

            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }
        }

        public static IEnumerable<object[]> GetTrainCaseData()
        {
            var enumerator = GetCaseTestData(Strings.TrainCase, Tokens.TrainCase);

            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }
        }

        public static IEnumerator<object[]> GetCaseTestData(string[] strings, WordToken[][] tokens)
        {
            for (int i = 0; i < DATA_ARR_CAPACITY; i++)
            {
                yield return new object[] {
                    strings[i],
                    tokens[i]
                };
            }
        }

        [TestClass]
        public static class Strings
        {
            public static readonly string[] CamelCase = {
                "helloWorld",
                "fooBarBaz",
                "camelCase",
                "notPascalCase"
            };
            public static readonly string[] PascalCase = {
                "HelloWorld",
                "FooBarBaz",
                "CamelCase",
                "NotPascalCase"
            };
            public static readonly string[] SnakeCase = {
                "hello_world",
                "foo_bar_baz",
                "camel_case",
                "not_pascal_case"
            };
            public static readonly string[] ConstantCase = {
                "HELLO_WORLD",
                "FOO_BAR_BAZ",
                "CAMEL_CASE",
                "NOT_PASCAL_CASE"
            };
            public static readonly string[] KebabCase = {
                "hello-world",
                "foo-bar-baz",
                "camel-case",
                "not-pascal-case"
            };
            public static readonly string[] TrainCase = {
                "Hello-World",
                "Foo-Bar-Baz",
                "Camel-Case",
                "Not-Pascal-Case"
            };
        }

        [TestClass]
        public static class Tokens
        {
            public static readonly WordToken[][] CamelCase = {
                new[] {
                    new WordToken(0, "hello"),
                    new WordToken(5, "World")
                },
                new[] {
                    new WordToken(0, "foo"),
                    new WordToken(3, "Bar"),
                    new WordToken(6, "Baz"),
                },
                new[] {
                    new WordToken(0, "camel"),
                    new WordToken(5, "Case")
                },
                new[] {
                    new WordToken(0, "not"),
                    new WordToken(3, "Pascal"),
                    new WordToken(9, "Case")
                }
            };
            public static readonly WordToken[][] PascalCase = {
                new[] {
                    new WordToken(0, "Hello"),
                    new WordToken(5, "World")
                },
                new[] {
                    new WordToken(0, "Foo"),
                    new WordToken(3, "Bar"),
                    new WordToken(6, "Baz"),
                },
                new[] {
                    new WordToken(0, "Camel"),
                    new WordToken(5, "Case")
                },
                new[] {
                    new WordToken(0, "Not"),
                    new WordToken(3, "Pascal"),
                    new WordToken(9, "Case")
                }
            };
            public static readonly WordToken[][] SnakeCase = {
                new[] {
                    new WordToken(0, "hello"),
                    new WordToken(6, "world")
                },
                new[] {
                    new WordToken(0, "foo"),
                    new WordToken(4, "bar"),
                    new WordToken(8, "baz"),
                },
                new[] {
                    new WordToken(0, "camel"),
                    new WordToken(6, "case")
                },
                new[] {
                    new WordToken(0, "not"),
                    new WordToken(4, "pascal"),
                    new WordToken(11, "case")
                }
            };
            public static readonly WordToken[][] ConstantCase = {
                new[] {
                    new WordToken(0, "HELLO"),
                    new WordToken(6, "WORLD")
                },
                new[] {
                    new WordToken(0, "FOO"),
                    new WordToken(4, "BAR"),
                    new WordToken(8, "BAZ"),
                },
                new[] {
                    new WordToken(0, "CAMEL"),
                    new WordToken(6, "CASE")
                },
                new[] {
                    new WordToken(0, "NOT"),
                    new WordToken(4, "PASCAL"),
                    new WordToken(11, "CASE")
                }
            };
            public static readonly WordToken[][] KebabCase = {
                new[] {
                    new WordToken(0, "hello"),
                    new WordToken(6, "world")
                },
                new[] {
                    new WordToken(0, "foo"),
                    new WordToken(4, "bar"),
                    new WordToken(8, "baz"),
                },
                new[] {
                    new WordToken(0, "camel"),
                    new WordToken(6, "case")
                },
                new[] {
                    new WordToken(0, "not"),
                    new WordToken(4, "pascal"),
                    new WordToken(11, "case")
                }
            };
            public static readonly WordToken[][] TrainCase = {
                new[] {
                    new WordToken(0, "Hello"),
                    new WordToken(6, "World")
                },
                new[] {
                    new WordToken(0, "Foo"),
                    new WordToken(4, "Bar"),
                    new WordToken(8, "Baz"),
                },
                new[] {
                    new WordToken(0, "Camel"),
                    new WordToken(6, "Case")
                },
                new[] {
                    new WordToken(0, "Not"),
                    new WordToken(4, "Pascal"),
                    new WordToken(11, "Case")
                }
            };
        }
    }
}
