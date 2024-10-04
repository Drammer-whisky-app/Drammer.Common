using Drammer.Common.Extensions;
using FluentAssertions;

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
}