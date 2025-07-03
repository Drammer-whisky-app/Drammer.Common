using System.Text.RegularExpressions;

namespace Drammer.Common;

/// <summary>
/// The DRM id.
/// </summary>
public static partial class DrmId
{
    /// <summary>
    /// Gets the DRM id.
    /// </summary>
    /// <param name="input">
    /// The input.
    /// </param>
    /// <returns>
    /// The <see cref="int"/>. Null when not found.
    /// </returns>
    public static long? Parse(string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return null;
        }

        var sanitized = input.Trim().ToLower();

        var r = DrmRegex();
        var match = r.Match(sanitized);
        if (match.Groups.Count > 1 && long.TryParse(match.Groups[1].Value, out var value))
        {
            return value;
        }

        return null;
    }

    [GeneratedRegex("^drm(\\d+)")]
    private static partial Regex DrmRegex();
}