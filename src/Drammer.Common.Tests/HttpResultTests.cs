using System.Net;

namespace Drammer.Common.Tests;

public sealed class HttpResultTests
{
    private readonly Fixture _fixture = new();

    [Fact]
    public void Ok_ReturnsHttpResultWithHttpStatusCodeOk()
    {
        // act
        var result = HttpResult.Ok();

        // assert
        result.HttpStatusCode.Should().Be(HttpStatusCode.OK);
        result.IsHttp200Ok.Should().BeTrue();
    }

    [Fact]
    public void Ok_ReturnsHttpResult()
    {
        // arrange
        var value = "test";

        // act
        var result = HttpResult.Ok(value);

        // assert
        result.HttpStatusCode.Should().Be(HttpStatusCode.OK);
        result.Value.Should().Be(value);
        result.IsHttp200Ok.Should().BeTrue();
    }

    [Fact]
    public void NoContent_ReturnsHttpResultWithHttpStatusCodeNoContent()
    {
        // act
        var result = HttpResult.NoContent();

        // assert
        result.HttpStatusCode.Should().Be(HttpStatusCode.NoContent);
        result.IsSuccess.Should().BeTrue();
        result.IsHttp200Ok.Should().BeFalse();
    }

    [Fact]
    public void NoContent_ReturnsHttpResult()
    {
        // act
        var result = HttpResult.NoContent<string>();

        // assert
        result.HttpStatusCode.Should().Be(HttpStatusCode.NoContent);
        result.Value.Should().BeNull();
        result.IsSuccess.Should().BeTrue();
        result.IsHttp200Ok.Should().BeFalse();
    }

    [Fact]
    public void Unauthorized_ReturnsHttpResult()
    {
        // act
        var result = HttpResult.Unauthorized();

        // assert
        result.HttpStatusCode.Should().Be(HttpStatusCode.Unauthorized);
        result.IsHttp200Ok.Should().BeFalse();
    }

    [Fact]
    public void Unauthorized_WithErrorCode_ReturnsHttpResult()
    {
        // arrange
        var errorCode = "error";

        // act
        var result = HttpResult.Unauthorized(errorCode);

        // assert
        result.HttpStatusCode.Should().Be(HttpStatusCode.Unauthorized);
        result.ErrorCode.Should().Be(errorCode);
        result.IsHttp200Ok.Should().BeFalse();
    }

    [Fact]
    public void Unauthorized_Typed_ReturnsHttpResult()
    {
        // act
        var result = HttpResult.Unauthorized<string>();

        // assert
        result.HttpStatusCode.Should().Be(HttpStatusCode.Unauthorized);
        result.Value.Should().BeNull();
        result.IsHttp200Ok.Should().BeFalse();
    }

    [Fact]
    public void Unauthorized_TypedWithErrorCode_ReturnsHttpResult()
    {
        // arrange
        var errorCode = "error";

        // act
        var result = HttpResult.Unauthorized<string>(errorCode);

        // assert
        result.HttpStatusCode.Should().Be(HttpStatusCode.Unauthorized);
        result.ErrorCode.Should().Be(errorCode);
        result.Value.Should().BeNull();
        result.IsHttp200Ok.Should().BeFalse();
    }

    [Fact]
    public void BadRequest_ReturnsHttpResult()
    {
        // act
        var result = HttpResult.BadRequest();

        // assert
        result.HttpStatusCode.Should().Be(HttpStatusCode.BadRequest);
        result.IsHttp200Ok.Should().BeFalse();
    }

    [Fact]
    public void BadRequest_Typed_ReturnsHttpResult()
    {
        // act
        var result = HttpResult.BadRequest<string>();

        // assert
        result.HttpStatusCode.Should().Be(HttpStatusCode.BadRequest);
        result.Value.Should().BeNull();
        result.IsHttp200Ok.Should().BeFalse();
    }

    [Fact]
    public void NotFound_ReturnsHttpResult()
    {
        // act
        var result = HttpResult.NotFound();

        // assert
        result.HttpStatusCode.Should().Be(HttpStatusCode.NotFound);
        result.IsHttp200Ok.Should().BeFalse();
    }

    [Fact]
    public void NotFound_Typed_ReturnsHttpResult()
    {
        // act
        var result = HttpResult.NotFound<string>();

        // assert
        result.HttpStatusCode.Should().Be(HttpStatusCode.NotFound);
        result.Value.Should().BeNull();
        result.IsHttp200Ok.Should().BeFalse();
    }

    [Fact]
    public void Failure_ReturnsHttpResult()
    {
        // arrange
        var httpStatusCode = _fixture.Create<HttpStatusCode>();

        // act
        var result = HttpResult.Failure(httpStatusCode);

        // assert
        result.HttpStatusCode.Should().Be(httpStatusCode);
        result.IsHttp200Ok.Should().Be(httpStatusCode == HttpStatusCode.OK);
    }

    [Fact]
    public void Failure_Typed_ReturnsHttpResult()
    {
        // arrange
        var httpStatusCode = _fixture.Create<HttpStatusCode>();

        // act
        var result = HttpResult.Failure<string>(httpStatusCode);

        // assert
        result.HttpStatusCode.Should().Be(httpStatusCode);
        result.Value.Should().BeNull();
        result.IsHttp200Ok.Should().Be(httpStatusCode == HttpStatusCode.OK);
    }
}