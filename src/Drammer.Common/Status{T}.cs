using System.Diagnostics.CodeAnalysis;

namespace Drammer.Common;

public record Status<T> : Status
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Status{T}"/> class.
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
        : base(success, message, exception)
    {
        Success = success;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Status{T}"/> class.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    public Status(T value)
        : base(true)
    {
        Value = value;
        Success = true;
    }

    /// <summary>
    /// Gets the value.
    /// </summary>
    public T? Value { get; init; }

    /// <inheritdoc />
    [MemberNotNullWhen(true, nameof(Value))]
    public override bool Success { get; }
}