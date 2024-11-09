using System.Net;

namespace Drammer.Common.Tests;

public sealed class HttpValidationResultTests
{
    private readonly Fixture _fixture = new();

    [Fact]
    public void Create_WithValidationErrors_ReturnsHttpValidationResult()
    {
        // arrange
        var httpStatusCode = _fixture.Create<HttpStatusCode>();

        var validationErrors = new Dictionary<string, string[]>
        {
            { "key", new[] { "error" } }
        };

        // act
        var result = HttpValidationResult.Create(validationErrors);

        // assert
        result.HttpStatusCode.Should().Be(httpStatusCode);
        result.ValidationErrors.Should().BeEquivalentTo(validationErrors);
    }

    [Fact]
    public void Create_Typed_WithValidationErrors_ReturnsHttpValidationResult()
    {
        // arrange
        var httpStatusCode = _fixture.Create<HttpStatusCode>();

        var validationErrors = new Dictionary<string, string[]>
        {
            { "key", new[] { "error" } }
        };

        // act
        var result = HttpValidationResult.Create<string>(validationErrors);

        // assert
        result.HttpStatusCode.Should().Be(httpStatusCode);
        result.ValidationErrors.Should().BeEquivalentTo(validationErrors);
    }
}