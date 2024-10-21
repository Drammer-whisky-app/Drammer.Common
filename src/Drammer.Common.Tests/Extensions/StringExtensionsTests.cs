using Drammer.Common.Extensions;

namespace Drammer.Common.Tests.Extensions;

public sealed class StringExtensionsTests
{
    private readonly Fixture _fixture = new();

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

    [Fact]
    public void AddUrlParameters_WhenUrlIsNull_ReturnUrl()
    {
        // arrange
        var input = (string?)null;

        // act
        var result = input.AddUrlParameters(null);

        // assert
        result.Should().Be(input);
    }

    [Fact]
    public void AddUrlParameters_WhenParametersAreNull_ReturnUrl()
    {
        // arrange
        var input = _fixture.Create<string>();

        // act
        var result = input.AddUrlParameters(null);

        // assert
        result.Should().Be(input);
    }

    [Fact]
    public void AddUrlParameters_WithParameters_ReturnUrl()
    {
        // arrange
        var input = "https://localhost";

        // act
        var result = input.AddUrlParameters(new {a = "b", c = "d"});

        // assert
        result.Should().Be("https://localhost?a=b&c=d");
    }

    [Fact]
    public void AddUrlParameters_UrlContainsParametersWithParameters_ReturnUrl()
    {
        // arrange
        var input = "https://localhost?1=2";

        // act
        var result = input.AddUrlParameters(new {a = "b"});

        // assert
        result.Should().Be("https://localhost?1=2&a=b");
    }

    [Theory]
    [InlineData("http://localhost", "https://localhost")]
    [InlineData("//localhost", "https://localhost")]
    [InlineData("https://localhost", "https://localhost")]
    public void ToHttps_HttpsUrl(string input, string expected)
    {
        // act
        var result = input.ToHttps();

        // assert
        result.Should().Be(expected);
    }

    [Fact]
    public void ChangeTopLevelDomain_TldChanged()
    {
        // arrange
        const string Input = "https://localhost.com/asdf";

        // act
        var result = Input.ChangeTopLevelDomain("localhost.net");

        // assert
        result.Should().Be("https://localhost.net/asdf");
    }

    [Fact]
    public void RemoveHtmlTags_TagsRemoved()
    {
        // arrange
        const string Input = "<html><body>text<img src=\"\"/><p>text</p></body></html>";

        // act
        var result = Input.RemoveHtmlTags();

        // assert
        result.Should().Be("texttext");
    }

    [Theory]
    [InlineData("abc", "abc")]
    [InlineData("00abc0", "abc0")]
    [InlineData("  00abc   ", "abc")]
    public void RemoveLeadingZeros(string input, string expected)
    {
        Assert.Equal(expected, input.RemoveLeadingZeros());
    }

    [Theory]
    [InlineData("abc", 1)]
    [InlineData("abc.def", 1)]
    [InlineData("abc def", 2)]
    [InlineData("abc. def", 2)]
    public void CountWords_ReturnsWordCount(string text, int expected)
    {
        // act
        var result = text.CountWords();

        // assert
        result.Should().Be(expected);
    }
}