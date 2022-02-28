using Case.NET.Parsing.WordSplitting;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Case.NET.Test.Parsing.WordSplitting
{
    [TestClass]
    public class TrailingNumberWordSplitterTest
    {
        [DataTestMethod]
        [DataRow("helloWorld_", 0, -1)]
        [DataRow("Hello World!", 0, -1)]
        [DataRow("hello123world", 0, 8)]
        [DataRow("_hello123My-0World", 0, 9)]
        [DataRow("_hello1230World", 6, -1)]
        [DataRow("Hello100500World", 5, -1)]
        public void TestTryFindSplitIndex(string value, int startAt, int expectedIndex)
        {
            const bool ExpectedSkipIndexChar =
                false; // TrailingNumber splitter should not skip chars in any case

            var actualIndex = TrailingNumberWordSplitter.Instance.TryFindSplitIndex(
                value,
                startAt,
                out bool actualSkipIndexChar
            );

            actualIndex.Should().Be(expectedIndex);
            actualSkipIndexChar.Should().Be(ExpectedSkipIndexChar);
        }
    }
}
