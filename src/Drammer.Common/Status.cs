namespace Drammer.Common;

public record Status
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Status"/> class.
    /// </summary>
    /// <param name="success">
    /// The success.
    /// </param>
    /// <param name="message">
    /// The message.
    /// </param>
    /// <param name="exception">
    /// The exception.
    /// </param>
    public Status(bool success, string? message = null, Exception? exception = null)
    {
        Success = success;
        Message = message;
        Exception = exception;
    }

    /// <summary>
    /// Gets or sets a value indicating whether success.
    /// </summary>
    public virtual bool Success { get; }

    /// <summary>
    /// Gets or sets the message.
    /// </summary>
    public string? Message { get; }

    /// <summary>
    /// Gets or sets the exception.
    /// </summary>
    public Exception? Exception { get; }
}