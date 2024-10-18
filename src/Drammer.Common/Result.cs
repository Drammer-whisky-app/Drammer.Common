namespace Drammer.Common;

public record Result
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Result"/> class.
    /// </summary>
    /// <param name="isSuccess">
    /// The success.
    /// </param>
    /// <param name="message">
    /// The message.
    /// </param>
    /// <param name="exception">
    /// The exception.
    /// </param>
    protected internal Result(bool isSuccess, string? message = null, Exception? exception = null)
    {
        IsSuccess = isSuccess;
        Message = message;
        Exception = exception;
    }

    /// <summary>
    /// Gets or sets a value indicating whether success.
    /// </summary>
    public virtual bool IsSuccess { get; }

    /// <summary>
    /// Gets or sets the message.
    /// </summary>
    public string? Message { get; }

    /// <summary>
    /// Gets or sets the exception.
    /// </summary>
    public Exception? Exception { get; }

    public static Result Success() => new (true);

    public static Result<T> Success<T>(T value) => new (value);

    public static ResultCode<T> Success<T>(T value, int code) => new (value, code);

    public static Result Failure(string? message = null, Exception? exception = null) => new(false, message, exception);

    public static Result<T> Failure<T>(string? message = null, Exception? exception = null) => new(false, message, exception);

    public static ResultCode<T> Failure<T>(int code, string? message = null, Exception? exception = null) => new(false, code, message, exception);

    public static Result Create(bool isSuccess) => isSuccess ? Success() : Failure();

    public static Result<T> Create<T>(T? value) => value != null ? Success(value) : Failure<T>();
}