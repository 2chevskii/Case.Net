using Case.NET.Parsing.Tokens;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Case.NET.Test.Parsing.Tokens
{
    [TestClass]
    public class WordTokenTest
    {
        [DataTestMethod]
        [DataRow(0, "hello", 5)]
        [DataRow(6, "world", 5)]
        [DataRow(12, "hello world", 11)]
        public void TestWordTokenCtor(int index, string value, int length)
        {
            var token = new WordToken(index, value, length);

            token.Index.Should().Be(index);
            token.Value.Should().Be(value);
            token.Length.Should().Be(length);
            token.EndIndex.Should().Be(index + length - 1);
        }

        [DataTestMethod]
        [DataRow(0, "hello")]
        [DataRow(6, "world")]
        [DataRow(12, "hello world")]
        public void TestWordTokenCtorShort(int index, string value)
        {
            var token = new WordToken(index, value);

            token.Index.Should().Be(index);
            token.Value.Should().Be(value);
            token.Length.Should().Be(value.Length);
            token.EndIndex.Should().Be(index + value.Length - 1);
        }
    }
}
