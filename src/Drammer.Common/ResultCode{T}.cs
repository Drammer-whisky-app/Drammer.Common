namespace Drammer.Common;

public sealed record ResultCode<T> : Result<T>
{
    internal ResultCode(bool isSuccess, int code, string? message = null, Exception? exception = null) : base(
        isSuccess,
        message,
        exception)
    {
        Code = code;
    }

    internal ResultCode(T value, int code = 0) : base(value)
    {
        Code = code;
    }

    /// <summary>
    /// Gets the code.
    /// </summary>
    public int Code { get; }
}