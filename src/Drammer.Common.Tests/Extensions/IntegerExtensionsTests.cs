using Drammer.Common.Extensions;

namespace Drammer.Common.Tests.Extensions;

public sealed class IntegerExtensionsTests
{
    [Theory]
    [InlineData(9, 10, 0)]
    [InlineData(11, 10, 10)]
    [InlineData(1531, 100, 1500)]
    public void FloorToNearestMultipleOf_WhenValueIsZero_ShouldReturnZero(int input, int nearest, int expected)
    {
        // act
        var result = input.FloorToNearest(nearest);

        // assert
        result.Should().Be(expected);
    }

    [Fact]
    public void FloorToNearestMultipleOf_WhenValueIsNegative_ShouldThrowArgumentOutOfRangeException()
    {
        // arrange
        var input = -9;
        var nearest = -10;

        // act
        Action act = () => input.FloorToNearest(nearest);

        // assert
        act.Should().Throw<ArgumentOutOfRangeException>();
    }
}