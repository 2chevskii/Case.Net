using Case.NET.Parsing.WordSplitting;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Case.NET.Test.Parsing.WordSplitting
{
    [TestClass]
    public class CamelCaseWordSplitterTest
    {
        [DataTestMethod]
        [DataRow("hello world", 0, -1)]
        [DataRow("hello_world", 0, -1)]
        [DataRow("_hello_world", 0, -1)]
        [DataRow("-hello_world", 0, -1)]
        [DataRow("hello-world", 0, -1)]
        [DataRow("hello - wo rld", 0, -1)]
        [DataRow("helloWorld", 0, 5)]
        [DataRow("GoodByeWorld", 0, 4)]
        public void TestTryFindSplitIndex(string value , int startAt, int expectedIndex)
        {
            const bool ExpectedSkipIndexChar = false; // CamelCase splitter should not skip chars in any case

            var actualIndex = CamelCaseWordSplitter.Instance.TryFindSplitIndex(
                value,
                startAt,
                out bool actualSkipIndexChar
            );

            actualIndex.Should().Be(expectedIndex);
            actualSkipIndexChar.Should().Be(ExpectedSkipIndexChar);
        }
    }
}
