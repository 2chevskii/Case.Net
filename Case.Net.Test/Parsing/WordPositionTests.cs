using Case.Net.Parsing;

using FluentAssertions;

namespace Case.Net.Test.Parsing;

[TestClass]
public class WordPositionTests
{
    [DataTestMethod]
    [DataRow( 0, 0 )]
    [DataRow( 1, 1 )]
    [DataRow( 1, 2 )]
    [DataRow( 12, 42 )]
    [DataRow( 0, 100 )]
    public void CtorTest(int wordEnd, int delimiterEnd)
    {
        Assert.That.Invoking( _ => new WordPosition( wordEnd, delimiterEnd ) )
              .Should()
              .NotThrow()
              .And.Match( wp => wp().WordEnd == wordEnd )
              .And.Match( wp => wp().DelimiterEnd == delimiterEnd )
              .And.Match(
                  wp => delimiterEnd > wordEnd
                        ? wp().HasDelimiter == true
                        : wp().HasDelimiter == false
              );
    }

    [DataTestMethod]
    [DataRow( -1, 0 )]
    [DataRow( -1, -1 )]
    [DataRow( -1, -3 )]
    [DataRow( 2, 1 )]
    [DataRow( 12, 7 )]
    [DataRow( 176123, 7623 )]
    [DataRow( 15, -67123 )]
    public void CtorTestFail(int wordEnd, int delimiterEnd)
    {
        Assert.That.Invoking( _ => new WordPosition( wordEnd, delimiterEnd ) )
              .Should()
              .Throw<ArgumentException>();
    }
}
