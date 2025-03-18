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
    public void FromString(string? input, string expectedValue)
    {
        // Act
        var result = Language.FromString(input);

        // Assert
        result.Value.Should().Be(expectedValue);
    }
}