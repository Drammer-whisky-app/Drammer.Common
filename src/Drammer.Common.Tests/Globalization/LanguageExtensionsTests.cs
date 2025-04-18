﻿using Drammer.Common.Globalization;

namespace Drammer.Common.Tests.Globalization;

public sealed class LanguageExtensionsTests
{
    [Fact]
    public void IsEnglish_WhenEnglish_ReturnsTrue()
    {
        // Arrange
        var language = Language.ToLanguageOrNull("en") ?? throw new InvalidOperationException("Language not found");

        // Act
        var result = language.IsEnglish();

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsEnglish_WhenDutch_ReturnsFalse()
    {
        // Arrange
        var language = Language.ToLanguageOrNull("nl") ?? throw new InvalidOperationException("Language not found");

        // Act
        var result = language.IsEnglish();

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void IsGerman_WhenGerman_ReturnsTrue()
    {
        // Arrange
        var language = Language.ToLanguageOrNull("de") ?? throw new InvalidOperationException("Language not found");

        // Act
        var result = language.IsGerman();

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsGerman_WhenDutch_ReturnsFalse()
    {
        // Arrange
        var language = Language.ToLanguageOrNull("nl") ?? throw new InvalidOperationException("Language not found");

        // Act
        var result = language.IsGerman();

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void IsDutch_WhenDutch_ReturnsTrue()
    {
        // Arrange
        var language = Language.ToLanguageOrNull("nl") ?? throw new InvalidOperationException("Language not found");

        // Act
        var result = language.IsDutch();

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsDutch_WhenGerman_ReturnsFalse()
    {
        // Arrange
        var language = Language.ToLanguageOrNull("de") ?? throw new InvalidOperationException("Language not found");

        // Act
        var result = language.IsDutch();

        // Assert
        result.Should().BeFalse();
    }
}