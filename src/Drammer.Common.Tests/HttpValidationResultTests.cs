using System.Net;

namespace Drammer.Common.Tests;

public sealed class HttpValidationResultTests
{
    private readonly Fixture _fixture = new();

    [Fact]
    public void CreateFromDictionary_WithValidationErrors_ReturnsHttpValidationResult()
    {
        // arrange
        var httpStatusCode = _fixture.Create<HttpStatusCode>();

        var validationErrors = new Dictionary<string, string[]>
        {
            { "key", ["error"]}
        };

        // act
        var result = HttpValidationResult.CreateFromDictionary(validationErrors, httpStatusCode);

        // assert
        result.HttpStatusCode.Should().Be(httpStatusCode);
        result.ValidationErrors.Should().BeEquivalentTo(validationErrors);
    }

    [Fact]
    public void Create_WithKeyAndValue_ReturnsHttpValidationResult()
    {
        // arrange
        var httpStatusCode = _fixture.Create<HttpStatusCode>();
        var errorKey = _fixture.Create<string>();
        var errorValue = _fixture.Create<string>();

        var expectedValidationError = new Dictionary<string, string[]>
        {
            { errorKey, [errorValue]}
        };

        // act
        var result = HttpValidationResult.Create(errorKey, errorValue, httpStatusCode);

        // assert
        result.HttpStatusCode.Should().Be(httpStatusCode);
        result.ValidationErrors.Should().BeEquivalentTo(expectedValidationError);
    }

    [Fact]
    public void Create_Typed_WithValidationErrors_ReturnsHttpValidationResult()
    {
        // arrange
        var httpStatusCode = _fixture.Create<HttpStatusCode>();

        var validationErrors = new Dictionary<string, string[]>
        {
            { "key", ["error"]}
        };

        // act
        var result = HttpValidationResult<string>.Create(validationErrors, httpStatusCode);

        // assert
        result.HttpStatusCode.Should().Be(httpStatusCode);
        result.ValidationErrors.Should().BeEquivalentTo(validationErrors);
    }
}