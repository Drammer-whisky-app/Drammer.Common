using System.Net;

namespace Drammer.Common;

/// <summary>
/// The HTTP result.
/// </summary>
public record HttpResult : Result
{
    protected internal HttpResult(
        bool isSuccess,
        HttpStatusCode httpStatusCode,
        string? errorCode = null,
        string? message = null,
        Exception? exception = null) : base(
        isSuccess,
        message,
        exception)
    {
        HttpStatusCode = httpStatusCode;
        ErrorCode = errorCode;
    }

    /// <summary>
    /// Gets the HTTP status code.
    /// </summary>
    public HttpStatusCode HttpStatusCode { get; }

    /// <summary>
    /// Gets the error code.
    /// </summary>
    public string? ErrorCode { get; }

    /// <summary>
    /// Gets a value indicating whether the HTTP status code is 200 OK.
    /// </summary>
    public virtual bool IsHttp200Ok => HttpStatusCode == HttpStatusCode.OK;

    /// <summary>
    /// HTTP OK.
    /// </summary>
    /// <param name="httpStatusCode">The HTTP status code.</param>
    /// <returns></returns>
    public static HttpResult Ok(HttpStatusCode httpStatusCode = HttpStatusCode.OK) => new(true, httpStatusCode);

    /// <summary>
    /// HTTP No Content.
    /// </summary>
    /// <returns></returns>
    public static HttpResult NoContent() => new(true, HttpStatusCode.NoContent);

    /// <summary>
    /// Http OK.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="httpStatusCode">The HTTP status code.</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static HttpResult<T> Ok<T>(T value, HttpStatusCode httpStatusCode = HttpStatusCode.OK) => new(value, httpStatusCode);

    /// <summary>
    /// Http No Content.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static HttpResult<T> NoContent<T>() => new(true, HttpStatusCode.NoContent);

    /// <summary>
    /// HTTP Unauthorized.
    /// </summary>
    /// <param name="errorCode"></param>
    /// <returns></returns>
    public static HttpResult Unauthorized(string? errorCode = null) => Failure(HttpStatusCode.Unauthorized, errorCode, "Unauthorized request");

    /// <summary>
    /// HTTP Unauthorized.
    /// </summary>
    /// <param name="errorCode"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static HttpResult<T> Unauthorized<T>(string? errorCode = null) => Failure<T>(HttpStatusCode.Unauthorized, errorCode, "Unauthorized request");

    /// <summary>
    /// HTTP Bad Request.
    /// </summary>
    /// <param name="errorCode"></param>
    /// <returns></returns>
    public static HttpResult BadRequest(string? errorCode = null) => Failure(HttpStatusCode.BadRequest, errorCode, "Bad request");

    /// <summary>
    /// HTTP Bad Request.
    /// </summary>
    /// <param name="errorCode"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static HttpResult<T> BadRequest<T>(string? errorCode = null) => Failure<T>(HttpStatusCode.BadRequest, errorCode, "Bad request");

    /// <summary>
    /// HTTP Not Found.
    /// </summary>
    /// <param name="errorCode"></param>
    /// <returns></returns>
    public static HttpResult NotFound(string? errorCode = null) => Failure(HttpStatusCode.NotFound, errorCode, "Not found");

    /// <summary>
    /// HTTP Not Found.
    /// </summary>
    /// <param name="errorCode"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static HttpResult<T> NotFound<T>(string? errorCode = null) => Failure<T>(HttpStatusCode.NotFound, errorCode, "Not found");

    /// <summary>
    /// HTTP failure.
    /// </summary>
    /// <param name="httpStatusCode"></param>
    /// <param name="errorCode"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public static HttpResult Failure(HttpStatusCode httpStatusCode, string? errorCode = null, string? message = null) =>
        new(false, httpStatusCode, errorCode, message);

    /// <summary>
    /// HTTP failure.
    /// </summary>
    /// <param name="httpStatusCode"></param>
    /// <param name="errorCode"></param>
    /// <param name="message"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static HttpResult<T> Failure<T>(HttpStatusCode httpStatusCode, string? errorCode = null, string? message = null) =>
        new(false, httpStatusCode, errorCode, message);
}