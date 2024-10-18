using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Drammer.Common.Extensions;

public static partial class StringExtensions
{
    [return: NotNullIfNotNull(nameof(emailAddress))]
    public static string? ObfuscateEmailAddress(this string? emailAddress)
    {
        if (string.IsNullOrWhiteSpace(emailAddress) || !emailAddress.Contains('@'))
        {
            return emailAddress;
        }

        var split = emailAddress.Split('@');
        if (split.Length != 2)
        {
            return emailAddress;
        }

        var sb = new StringBuilder();
        if (split[0].Length <= 3)
        {
            sb.Append(split[0][0] + new string('*', split[0].Length - 1));
        }
        else
        {
            sb.Append(split[0].Substring(0, 2) + new string('*', split[0].Length - 2));
        }

        sb.Append("@" + split[1]);
        return sb.ToString();
    }

    public static decimal? ToDecimal(this string? value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return null;
        }

        // does the string contains dots and commas?
        var dotIndex = value.IndexOf(".", StringComparison.OrdinalIgnoreCase);
        var commaIndex = value.IndexOf(",", StringComparison.InvariantCultureIgnoreCase);

        if (dotIndex >= 0)
        {
            if (commaIndex >= 0)
            {
                // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
                if (commaIndex > dotIndex)
                {
                    // comma after the dot: 99.999,5
                    value = value.Replace(".", string.Empty).Replace(",", ".");
                }
                else
                {
                    // comma before the dot: 99,999.5
                    value = value.Replace(",", string.Empty);
                }
            }
        }
        else if (commaIndex >= 0)
        {
            value = value.Replace(",", ".");
        }

        if (!decimal.TryParse(value, NumberStyles.Any, new CultureInfo("en-US"), out var result))
        {
            return null;
        }

        return result;
    }

    [return: NotNullIfNotNull(nameof(input))]
    public static string? SanitizeText(this string? input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return null;
        }

        return SanitizeRegex().Replace(input, string.Empty).Trim();
    }

    /// <summary>
    /// Adds parameters to a URL.
    /// </summary>
    /// <param name="url">
    /// The url.
    /// </param>
    /// <param name="parameters">
    /// The parameters.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    [return: NotNullIfNotNull(nameof(url))]
    public static string? AddUrlParameters(this string? url, object? parameters)
    {
        if (url == null)
        {
            return null;
        }

        if (parameters == null)
        {
            return url;
        }

        var properties = parameters.GetType().GetProperties();
        if (properties.IsNullOrEmpty())
        {
            return url;
        }

        var pairs = properties.Select(x => $"{x.Name}={x.GetValue(parameters, null)}");

        if (url.Contains('?'))
        {
            url += "&";
        }
        else
        {
            url += "?";
        }

        return url + string.Join("&", pairs);
    }

    /// <summary>
    /// Converts the URL to an https URL.
    /// </summary>
    /// <param name="input">
    /// The input.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    [return: NotNullIfNotNull(nameof(input))]
    public static string? ToHttps(this string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return input;
        }

        var result = input.Trim().ToLower();
        if (result.StartsWith("http:"))
        {
            return result.Replace("http:", "https:");
        }

        if (result.StartsWith("//"))
        {
            return $"https:{result}";
        }

        return input;
    }

    [return: NotNullIfNotNull(nameof(input))]
    public static string? ChangeTopLevelDomain(this string? input, string tld)
    {
        if (string.IsNullOrWhiteSpace(tld))
        {
            throw new ArgumentNullException(nameof(tld));
        }

        if (string.IsNullOrWhiteSpace(input))
        {
            return input;
        }

        var url = input.ToLower();
        if (url.StartsWith("//"))
        {
            url = $"https:{url}";
        }

        var uri = new Uri(url);
        return input.ToLower().Replace(uri.Host, tld.ToLower());
    }

    [return: NotNullIfNotNull(nameof(input))]
    public static string? RemoveHtmlTags(this string? input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }

        // based on: http://stackoverflow.com/questions/19523913/remove-html-tags-from-string-including-nbsp-in-c-sharp
        // remove tags
        var noHtml = RemoveHtmlTagsRegex().Replace(input, string.Empty).Trim();

        // normalize
        return NormalizeHtmlRegex().Replace(noHtml, " ");
    }

    [GeneratedRegex("[^\\w\\d\\s.,+-\\\\&#$%^(()\"'!*|\\[\\]]", RegexOptions.IgnoreCase)]
    private static partial Regex SanitizeRegex();

    [GeneratedRegex("<[^>]+>|&nbsp;")]
    private static partial Regex RemoveHtmlTagsRegex();

    [GeneratedRegex("\\s{2,}")]
    private static partial Regex NormalizeHtmlRegex();
}