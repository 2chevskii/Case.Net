using Case.NET.Parsing.WordSplitting;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Case.NET.Test.Parsing.WordSplitting
{
    [TestClass]
    public class UpperLowerCaseWordSplitterTest
    {
        [DataTestMethod]
        [DataRow("helloWorld", 0, -1)]
        [DataRow("HelloWorld", 0, -1)]
        [DataRow("HELLOworld", 0, 5)]
        [DataRow("HElloworld", 0, 2)]
        [DataRow("WORLDhello", 5, -1)]
        [DataRow("Hello_world", 0, -1)]
        [DataRow("HELLO_world", 0, -1)]
        public void TestTryFindSplitIndex(string value, int startAt, int expectedIndex)
        {
            const bool ExpectedSkipIndexChar =
                false; // UpperLowerCase splitter should not skip chars in any case

            var actualIndex = UpperLowerCaseWordSplitter.Instance.TryFindSplitIndex(
                value,
                startAt,
                out bool actualSkipIndexChar
            );

            actualIndex.Should().Be(expectedIndex);
            actualSkipIndexChar.Should().Be(ExpectedSkipIndexChar);
        }
    }
}
