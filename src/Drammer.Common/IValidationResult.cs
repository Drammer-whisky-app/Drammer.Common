namespace Drammer.Common;

/// <summary>
/// The validation result.
/// </summary>
public interface IValidationResult
{
    /// <summary>
    /// Gets the validation errors.
    /// </summary>
    IDictionary<string, string[]>? ValidationErrors { get; }
}