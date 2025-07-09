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
        var status = new Result<string>(success, message, exception);

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
        var status = new Result<string>(success, message, exception);

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
        var status = new Result<string>(success, message, exception);

        // assert
        status.IsSuccess.Should().BeTrue();
        status.Message.Should().Be(message);
        status.Exception.Should().Be(exception);
    }

    [Fact]
    public void Construct_ValueNotNull_SuccessTrue()
    {
        // arrange
        var value = "value";

        // act
        var status = new Result<string>(value);

        // assert
        status.IsSuccess.Should().BeTrue();
        status.Value.Should().Be(value);
    }

    [Fact]
    public void Success_ValueNotNull_SuccessTrue()
    {
        // arrange
        var value = "value";

        // act
        var status = Result.Success(value);

        // assert
        status.IsSuccess.Should().BeTrue();
        status.Value.Should().Be(value);
    }

    [Fact]
    public void Failure_MessageNotNullAndExceptionNotNull_SuccessFalse()
    {
        // arrange
        var message = "message";
        var exception = new Exception();

        // act
        var status = Result.Failure<string>(message, exception);

        // assert
        status.IsSuccess.Should().BeFalse();
        status.Message.Should().Be(message);
        status.Exception.Should().Be(exception);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Create_SuccessTrue_MessageNullAndExceptionNull(string? value)
    {
        // arrange
        var expectedIsSuccessValue = value != null;

        // act
        var status = Result.Create(value);

        // assert
        status.IsSuccess.Should().Be(expectedIsSuccessValue);
        status.Message.Should().BeNull();
        status.Exception.Should().BeNull();
    }
}