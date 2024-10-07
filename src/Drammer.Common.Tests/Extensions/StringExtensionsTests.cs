using Drammer.Common.Extensions;
using FluentAssertions;

namespace Drammer.Common.Tests.Extensions;

public sealed class StringExtensionsTests
{
    [Theory]
    [InlineData("test@drammer.com", "te**@drammer.com")]
    [InlineData("tes@drammer.com", "t**@drammer.com")]
    [InlineData("longaddress@drammer.com", "lo*********@drammer.com")]
    [InlineData("", "")]
    [InlineData("test@drammer@.com", "test@drammer@.com")]
    public void ObfuscateEmailAddress_ReturnObfuscatedEmailAddress(string input, string expected)
    {
        // act
        var result = input.ObfuscateEmailAddress();

        // assert
        result.Should().NotBeNull();
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("1", 1)]
    [InlineData("-1", -1)]
    [InlineData("1.6", 1.6)]
    [InlineData("-1.6", -1.6)]
    [InlineData("1,6", 1.6)]
    [InlineData("-1,6", -1.6)]
    [InlineData("1.000,05", 1000.05)]
    [InlineData("1,000.12", 1000.12)]
    public void ToDecimal_ReturnsDecimalType(string input, decimal expected)
    {
        // act
        var result = input.ToDecimal();

        // assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void ToDecimal_ReturnsNull(string? input)
    {
        // act
        var result = input.ToDecimal();

        // assert
        result.Should().BeNull();
    }

    [Theory]
    [InlineData("abc123", "abc123")]
    [InlineData("abc123!@#$%^&*()", "abc123!@#$%^&*()")]
    public void SanitizeText_ReturnsSanitizedText(string input, string expected)
    {
        // act
        var result = input.SanitizeText();

        // assert
        result.Should().Be(expected);
    }
}