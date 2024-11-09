using System.Net;

namespace Drammer.Common;

/// <summary>
/// The HTTP validation result.
/// </summary>
public sealed record HttpValidationResult<T> : HttpResult<T>, IValidationResult
{
    internal HttpValidationResult(
        IDictionary<string, string[]>? validationErrors,
        HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest,
        string? errorCode = null,
        string? message = null,
        Exception? exception = null) : base(false, httpStatusCode, errorCode, message, exception)
    {
        ValidationErrors = validationErrors;
    }

    /// <inheritdoc />
    public IDictionary<string, string[]>? ValidationErrors { get; }
}