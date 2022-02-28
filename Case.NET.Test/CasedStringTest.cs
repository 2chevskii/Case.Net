using System;
using System.Collections.Generic;

using Case.NET.Parsing.Tokens;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Case.NET.Test
{
    [TestClass]
    public class CasedStringTest
    {
        public static IEnumerable<object[]> InvalidConstructorValues
        {
            get
            {
                yield return new object[] {
                    "hello World",
                    "helloWorld",
                    new WordToken[] {
                        new WordToken(0, "hello"),
                        new WordToken(5, "World")
                    },
                    null
                };

                yield return new object[] {
                    null,
                    null,
                    null,
                    CaseConverter.PascalCase
                };

                yield return new object[] {
                    "HelloWorld",
                    "Hello-World",
                    null,
                    CaseConverter.TrainCase
                };
            }
        }

        public static IEnumerable<object[]> ValidConstructorValues
        {
            get
            {
                yield return new object[] {
                    "Hello World",
                    "helloWorld",
                    new WordToken[] {
                        new WordToken(0, "Hello"),
                        new WordToken(6, "World")
                    },
                    CaseConverter.CamelCase
                };
            }
        }

        [DataTestMethod]
        [DataRow(
            null,
            null,
            null,
            null
        )]
        [DataRow(
            null,
            "helloWorld",
            null,
            null
        )]
        [DynamicData(nameof(InvalidConstructorValues), DynamicDataSourceType.Property)]
        public void TestCasedStringCtorInvalid(
            string originalValue,
            string value,
            IReadOnlyList<WordToken> tokens,
            ICaseConverter converter
        )
        {
            Func<CasedString> ctorFunc = () => new CasedString(
                originalValue,
                value,
                tokens,
                converter
            );

            ctorFunc.Should().ThrowExactly<ArgumentNullException>();
        }

        [DataTestMethod]
        [DynamicData(nameof(ValidConstructorValues))]
        public void TestCasedStringCtorValid(
            string originalValue,
            string value,
            IReadOnlyList<WordToken> tokens,
            ICaseConverter converter
        )
        {
            Func<CasedString> ctorFunc = () => new CasedString(
                originalValue,
                value,
                tokens,
                converter
            );

            var casedString = ctorFunc.Should().NotThrow().And.Subject.Invoke();

            casedString.OriginalValue.Should().Be(originalValue);
            casedString.Value.Should().Be(value);
            casedString.Tokens.Should().BeEquivalentTo(tokens);
            casedString.Converter.Should().Be(converter);
        }
    }
}
