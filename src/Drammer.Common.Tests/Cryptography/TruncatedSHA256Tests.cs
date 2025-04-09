using Drammer.Common.Cryptography;

namespace Drammer.Common.Tests.Cryptography;

public sealed class TruncatedSHA256Tests
{
    [Theory]
    [InlineData("test", "n4bQgYhMfWWaL+qgxVrQFQ==")]
    public void ComputeToBase64_ReturnsResult(string input, string expected)
    {
        // Act
        var result = TruncatedSHA256.ComputeToBase64(input);

        // Assert
        result.Should().NotBeNull();
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("test", "9F86D081884C7D659A2FEAA0C55AD015")]
    public void ComputeToHex_ReturnsResult(string input, string expected)
    {
        // Act
        var result = TruncatedSHA256.ComputeToHex(input);

        // Assert
        result.Should().NotBeNull();
        result.Should().Be(expected);
    }
}