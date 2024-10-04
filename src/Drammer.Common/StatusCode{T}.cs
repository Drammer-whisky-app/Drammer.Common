namespace Drammer.Common;

public sealed record StatusCode<T> : Status<T>
{
    public StatusCode(bool success, int code, string? message = null, Exception? exception = null) : base(
        success,
        message,
        exception)
    {
        Code = code;
    }

    public StatusCode(T value, int code = 0) : base(value)
    {
        Code = code;
    }

    /// <summary>
    /// Gets the code.
    /// </summary>
    public int Code { get; }
}