using Case.Net.Common;

using FluentAssertions;

namespace Case.Net.Test;

[TestClass] public class WordTests { }

[TestClass]
public class PositionTests
{

    [TestMethod]
    public void StartGreaterThanEnd()
    {
        Assert.That.Invoking( _ => new Position( 2, 1 ) )
              .Should()
              .ThrowExactly<ArgumentException>();
    }

    [TestMethod]
    public void StartLessThanZero()
    {
        Assert.That.Invoking( _ => new Position( -1, 1 ) )
              .Should()
              .ThrowExactly<ArgumentOutOfRangeException>();
    }

    [DataTestMethod]
    [DynamicData(nameof(GetTestPositions))]
    public void TestLength(Position position)
    {
        var validLength = position.End - position.Start + 1;
        position.Length.Should().Be( validLength );
    }

    public static IEnumerable<object[]> GetTestPositions()
    {
        yield return new object[] {new Position( 0, 1 )};
        yield return new object[] {new Position( 10, 42 )};
        yield return new object[] {new Position( 5, 15 )};
    }
}
