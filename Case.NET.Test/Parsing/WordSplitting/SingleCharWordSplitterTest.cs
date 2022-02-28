using Case.NET.Parsing.WordSplitting;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Case.NET.Test.Parsing.WordSplitting
{
    [TestClass]
    public class SingleCharWordSplitterTest
    {
        [DataTestMethod]
        [DataRow(' ', "hello world", 0, 5, true)]
        [DataRow('_', "hello_world", 0, 5, true)]
        [DataRow(' ', "hello world", 6, -1, false)]
        [DataRow('_', "_hello world", 1, -1, false)]
        [DataRow('-', "hello wo-rld", 0, 8, true)]
        public void TestTryFindSplitIndex(char splitChar, string value, int startAt, int expectedIndex, bool expectedSkipIndexChar)
        {
            var splitter = new SingleCharWordSplitter(splitChar);

            int actualIndex = splitter.TryFindSplitIndex(
                value,
                startAt,
                out bool actualSkipIndexChar
            );

            actualIndex.Should().Be(expectedIndex);
            actualSkipIndexChar.Should().Be(expectedSkipIndexChar);
        }
    }
}
