using System.Net;

namespace Drammer.Common;

/// <summary>
/// The HTTP validation result.
/// </summary>
public sealed class HttpValidationResult : HttpResult, IValidationResult
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

    /// <summary>
    /// Create a new instance of <see cref="HttpValidationResult"/>.
    /// </summary>
    /// <param name="errors"></param>
    /// <param name="httpStatusCode"></param>
    /// <returns></returns>
    public static HttpValidationResult Create(
        IDictionary<string, string[]>? errors,
        HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest) => new(errors, httpStatusCode);

    /// <summary>
    /// Create a new instance of <see cref="HttpValidationResult"/>.
    /// </summary>
    /// <param name="errorKey">The error key.</param>
    /// <param name="errorValue">The error value.</param>
    /// <param name="httpStatusCode">The HTTP status code.</param>
    /// <returns></returns>
    public static HttpValidationResult Create(
        string errorKey,
        string errorValue,
        HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest) => new(
        new Dictionary<string, string[]>
        {
            {errorKey, [errorValue]}
        },
        httpStatusCode);
}