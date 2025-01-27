using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace Drammer.Common;

/// <summary>
/// The HTTP result.
/// </summary>
/// <typeparam name="T">The value type.</typeparam>
public record HttpResult<T> : HttpResult
{
    protected internal HttpResult(
        bool isSuccess,
        HttpStatusCode httpStatusCode,
        string? errorCode = null,
        string? message = null,
        Exception? exception = null) : base(isSuccess, httpStatusCode, errorCode, message, exception)
    {
        IsSuccess = isSuccess;
    }

    protected internal HttpResult(T value, HttpStatusCode httpStatusCode = HttpStatusCode.OK)
        : base(true, httpStatusCode)
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