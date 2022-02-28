using Case.NET.Parsing.Filters;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Case.NET.Test.Parsing.Filters
{
    [TestClass]
    public class CharFilterTest
    {

        [TestMethod("Should skip (single char filter) -> should return true")]
        [DataTestMethod]
        [DataRow(' ', "hello world", 5)]
        [DataRow(' ', " hello world", 0)]
        [DataRow(' ', "he llo world", 2)]
        [DataRow('-', "hello-world", 5)]
        [DataRow('-', "-hello world", 0)]
        [DataRow('-', "he-llo world", 2)]
        [DataRow('_', "hello_world", 5)]
        [DataRow('_', "_hello world", 0)]
        [DataRow('_', "he_llo world", 2)]
        [DataRow('.', "hello.world", 5)]
        [DataRow('.', ".hello world", 0)]
        [DataRow('.', "he.llo world", 2)]
        public void TestShouldSkipTrue(char skipChar, string value, int index)
        {
            const bool ExpectedResult = true;

            var filter = GetCharFilter(skipChar);

            bool actualResult = filter.ShouldSkip(value, index);

            actualResult.Should()
                        .Be(
                            ExpectedResult,
                            "ShouldSkip(string, int) should return TRUE on char {0} in string: {1}",
                            skipChar,
                            value
                        );
        }

        [TestMethod("Should skip (single char filter) -> should return false")]
        [DataTestMethod]
        [DataRow(' ', "hello world", 6)]
        [DataRow(' ', " hello world", 1)]
        [DataRow(' ', "he llo world", 3)]
        [DataRow('-', "hello-world", 6)]
        [DataRow('-', "-hello world", 1)]
        [DataRow('-', "he-llo world", 3)]
        [DataRow('_', "hello_world", 6)]
        [DataRow('_', "_hello world", 1)]
        [DataRow('_', "he_llo world", 3)]
        [DataRow('.', "hello.world", 6)]
        [DataRow('.', ".hello world", 1)]
        [DataRow('.', "he.llo world", 3)]
        public void TestShouldSkipFalse(char skipChar, string value, int index)
        {
            const bool ExpectedResult = false;

            var filter = GetCharFilter(skipChar);

            bool actualResult = filter.ShouldSkip(value, index);

            actualResult.Should()
                        .Be(
                            ExpectedResult,
                            "ShouldSkip(string, int) should return FALSE on char {0} in string: {1}",
                            skipChar,
                            value
                        );
        }

        [TestMethod("Should skip (multi char filter)")]
        [DataTestMethod]
        [DataRow(
            new[] {
                ',',
                '_'
            },
            ",hello_world ",
            new[] {
                0,
                6,
                7
            },
            new[] {
                true,
                true,
                false
            }
        )]
        [DataRow(
            new[] {
                '.',
                '/'
            },
            "hello //world.",
            new[] {
                0,
                1,
                5,
                6,
                7,
                8,
                13
            },
            new[] {
                false,
                false,
                false,
                true,
                true,
                false,
                true
            }
        )]
        public void TestShouldSkipMultiChar(
            char[] skipChars,
            string value,
            int[] indexes,
            bool[] expectedResults
        )
        {
            var filter = new CharFilter(skipChars);

            for (var i = 0; i < indexes.Length; i++)
            {
                int index = indexes[i];
                bool expected = expectedResults[i];

                bool actual = filter.ShouldSkip(value, index);

                actual.Should()
                      .Be(
                          expected,
                          "ShouldSkip(string, int) should return {0} for chars {1} on index {2} in string {3}",
                          expected,
                          $"[{string.Join(",", skipChars)}]",
                          index,
                          value
                      );
            }
        }

        static CharFilter GetCharFilter(char skipChar) => new CharFilter(skipChar);
    }
}
