using Case.Net.Parsing;

using FluentAssertions;

namespace Case.Net.Test;

[TestClass]
public class CamelCaseParserTests
{
    [TestMethod]
    public void TryParseTest()
    {
        var parser = new CamelCaseParser();

        parser.TryParse("camelCase", out var words).Should().BeTrue();
        var expected = new[] {new WordPosition( 4, 4 ), new WordPosition( 8, 8 )};
        words.Should().BeEquivalentTo( expected );
    }
}
