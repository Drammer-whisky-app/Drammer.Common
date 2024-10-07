using System.Diagnostics.CodeAnalysis;

namespace Drammer.Common;

public record Result<T> : Result
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Result{T}"/> class.
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
    internal Result(bool isSuccess, string? message = null, Exception? exception = null)
        : base(isSuccess, message, exception)
    {
        IsSuccess = isSuccess;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Result{T}"/> class.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    internal Result(T value)
        : base(true)
    {
        Value = value;
        IsSuccess = true;
    }

    /// <summary>
    /// Gets the value.
    /// </summary>
    public T? Value { get; init; }

    /// <inheritdoc />
    [MemberNotNullWhen(true, nameof(Value))]
    public override bool IsSuccess { get; }
}