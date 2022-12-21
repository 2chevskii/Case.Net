using Case.Net.Common.Conventions;
using Case.Net.Common.Entities;

using FluentAssertions;

namespace Case.Net.Test.Common.Conventions;

[TestClass]
public class DotCaseNamingConventionTests
{
    [TestMethod]
    public void TestConvert()
    {
        var input = new CasedString(
            string.Empty,
            string.Empty,
            new[] {"camel", "Cased", "Words"},
            Array.Empty<Delimiter>(),
            new CamelCaseNamingConvention()
        );

        var convention = new DotCaseNamingConvention();

        CasedString output = convention.Convert(input);

        output.Prefix.Should().BeEmpty();
        output.Suffix.Should().BeEmpty();
        output.Words.Should().BeEquivalentTo( "camel", "cased", "words" );
        output.Delimiters.Should()
              .BeEquivalentTo( new[] {new Delimiter( 0, "." ), new Delimiter( 1, "." )} );
        output.NamingConvention.Should().BeSameAs( convention );
    }
}
