namespace Drammer.Common.Tests;

public sealed class DrmIdTests
{
    [Theory]
    [InlineData("drm0001", 1)]
    [InlineData("drm1", 1)]
    [InlineData("drm999999", 999999)]
    [InlineData("drm", null)]
    [InlineData("", null)]
    [InlineData("dr999999", null)]
    [InlineData("asf1", null)]
    [InlineData(null, null)]
    public void Parse_ReturnsId(string? input, int? expected)
    {
        // act
        var result = DrmId.Parse(input);

        // assert
        result.Should().Be(expected);
    }
}