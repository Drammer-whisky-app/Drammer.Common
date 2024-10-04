using FluentAssertions;

namespace Drammer.Common.Tests;

public sealed class StatusCodeTypedTests
{
    [Fact]
    public void Construct_SuccessTrue_MessageNullAndExceptionNull()
    {
        // arrange
        var success = true;
        var message = (string?)null;
        var exception = (Exception?)null;
        var code = 2;

        // act
        var status = new StatusCode<string>(success, code, message, exception);

        // assert
        status.Success.Should().BeTrue();
        status.Message.Should().BeNull();
        status.Exception.Should().BeNull();
        status.Code.Should().Be(code);
    }

    [Fact]
    public void Construct_SuccessFalse_MessageNullAndExceptionNull()
    {
        // arrange
        var success = false;
        var message = (string?)null;
        var exception = (Exception?)null;
        var code = 3;

        // act
        var status = new StatusCode<string>(success, code, message, exception);

        // assert
        status.Success.Should().BeFalse();
        status.Message.Should().BeNull();
        status.Exception.Should().BeNull();
        status.Code.Should().Be(code);
    }

    [Fact]
    public void Construct_SuccessTrue_MessageNotNullAndExceptionNotNull()
    {
        // arrange
        var success = true;
        var message = "message";
        var exception = new Exception();
        var code = 4;

        // act
        var status = new StatusCode<string>(success, code, message, exception);

        // assert
        status.Success.Should().BeTrue();
        status.Message.Should().Be(message);
        status.Exception.Should().Be(exception);
        status.Code.Should().Be(code);
    }

    [Fact]
    public void Construct_ValueNotNull_SuccessTrue()
    {
        // arrange
        var value = "value";

        // act
        var status = new StatusCode<string>(value);

        // assert
        status.Success.Should().BeTrue();
        status.Value.Should().Be(value);
        status.Code.Should().Be(0);
    }

    [Fact]
    public void Construct_ValueNotNull_SuccessTrueAndCodeSet()
    {
        // arrange
        var value = "value";
        var code = 5;

        // act
        var status = new StatusCode<string>(value, code);

        // assert
        status.Success.Should().BeTrue();
        status.Value.Should().Be(value);
        status.Code.Should().Be(code);
    }
}