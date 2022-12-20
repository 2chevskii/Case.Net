using Case.Net.Parsing;

using FluentAssertions;

namespace Case.Net.Test.Parsing;

[TestClass]
public class CamelCaseParserTests
{
    [TestMethod]
    public void TryParseTest()
    {
        CamelCaseParser parser = new CamelCaseParser();

        parser.TryParse("camelCase", out IReadOnlyList<WordPosition> words).Should().BeTrue();
        WordPosition[] expected = new[] {new WordPosition( 4, 4 ), new WordPosition( 8, 8 )};
        words.Should().BeEquivalentTo( expected );
    }
}
