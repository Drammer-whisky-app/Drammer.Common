using FluentAssertions;

namespace Drammer.Common.Tests;

public sealed class ResultTests
{
    [Fact]
    public void Construct_SuccessTrue_MessageNullAndExceptionNull()
    {
        // arrange
        var success = true;
        var message = (string?)null;
        var exception = (Exception?)null;

        // act
        var status = new Result(success, message, exception);

        // assert
        status.IsSuccess.Should().BeTrue();
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
        var status = new Result(success, message, exception);

        // assert
        status.IsSuccess.Should().BeFalse();
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
        var status = new Result(success, message, exception);

        // assert
        status.IsSuccess.Should().BeTrue();
        status.Message.Should().Be(message);
        status.Exception.Should().Be(exception);
    }

    [Fact]
    public void Success_IsSuccessIsTrue()
    {
        // act
        var result = Result.Success();

        // assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public void Failure_IsSuccessIsFalse()
    {
        // act
        var result = Result.Failure();

        // assert
        result.IsSuccess.Should().BeFalse();
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Create_SuccessTrue_MessageNullAndExceptionNull(bool isSuccess)
    {
        // act
        var status = Result.Create(isSuccess);

        // assert
        status.IsSuccess.Should().Be(isSuccess);
        status.Message.Should().BeNull();
        status.Exception.Should().BeNull();
    }
}