using FluentAssertions;

namespace Drammer.Common.Tests;

public sealed class StatusTypedTests
{
    [Fact]
    public void Construct_SuccessTrue_MessageNullAndExceptionNull()
    {
        // arrange
        var success = true;
        var message = (string?)null;
        var exception = (Exception?)null;

        // act
        var status = new Status<string>(success, message, exception);

        // assert
        status.Success.Should().BeTrue();
        status.Message.Should().BeNull();
        status.Exception.Should().BeNull();
    }

    [Fact]
    public void Construct_SuccessFalse_MessageNullAndExceptionNull()
    {
        // arrange
        var success = false;
        var message = (string?)null;
        var exception = (Exception?)null;

        // act
        var status = new Status<string>(success, message, exception);

        // assert
        status.Success.Should().BeFalse();
        status.Message.Should().BeNull();
        status.Exception.Should().BeNull();
    }

    [Fact]
    public void Construct_SuccessTrue_MessageNotNullAndExceptionNotNull()
    {
        // arrange
        var success = true;
        var message = "message";
        var exception = new Exception();

        // act
        var status = new Status<string>(success, message, exception);

        // assert
        status.Success.Should().BeTrue();
        status.Message.Should().Be(message);
        status.Exception.Should().Be(exception);
    }

    [Fact]
    public void Construct_ValueNotNull_SuccessTrue()
    {
        // arrange
        var value = "value";

        // act
        var status = new Status<string>(value);

        // assert
        status.Success.Should().BeTrue();
        status.Value.Should().Be(value);
    }
}