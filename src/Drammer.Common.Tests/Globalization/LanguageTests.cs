using Drammer.Common.Globalization;

namespace Drammer.Common.Tests.Globalization;

public sealed class LanguageTests
{
    [Theory]
    [InlineData("EN", "en")]
    [InlineData("nl", "nl")]
    [InlineData("de", "de")]
    [InlineData("test", "en")]
    [InlineData("", "en")]
    [InlineData(null, "en")]
    public void ToLanguageOrDefault(string? input, string expectedValue)
    {
        // Act
        var result = Language.ToLanguageOrDefault(input);

        // Assert
        result.Value.Should().Be(expectedValue);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void ToLanguageOrNull_WithValidInput_ShouldReturnNull(string? input)
    {
        // Act
        var result = Language.ToLanguageOrNull(input);

        // Assert
        result.Should().BeNull();
    }
}