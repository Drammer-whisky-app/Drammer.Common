using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Extensions.ObjectPool;

namespace Drammer.Common.Extensions;

/// <summary>
/// The string extensions.
/// </summary>
public static partial class StringExtensions
{
    /// <summary>
    /// Obfuscates an email address.
    /// </summary>
    /// <param name="emailAddress">The email address.</param>
    /// <returns>A <see cref="string"/>.</returns>
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

        var pool = ObjectPool.Create<StringBuilder>();
        var sb = pool.Get();

        try
        {
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
        finally
        {
            pool.Return(sb);
        }
    }

    /// <summary>
    /// Convert a string to a decimal.
    /// </summary>
    /// <param name="value">The input value.</param>
    /// <returns>A <see cref="decimal"/> value.</returns>
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

    /// <summary>
    /// Removes all non-alphanumeric characters from a string.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <returns>A <see cref="string"/>.</returns>
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
    /// Removes leading zeros from a string.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <returns>A <see cref="string"/>.</returns>
    [return: NotNullIfNotNull(nameof(text))]
    public static string? RemoveLeadingZeros(this string? text)
    {
        return string.IsNullOrEmpty(text) ? text : text.Trim().TrimStart('0');
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

    /// <summary>
    /// Changes the top level domain of a URL.
    /// </summary>
    /// <param name="input">The input url.</param>
    /// <param name="tld">The top level domain.</param>
    /// <returns>A <see cref="string"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <see cref="tld"/> is invalid.</exception>
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

    /// <summary>
    /// Replaces values in a string.
    /// </summary>
    /// <param name="input"></param>
    /// <param name="replacements"></param>
    /// <returns></returns>
    [return: NotNullIfNotNull(nameof(input))]
    public static string? ReplaceValues(this string? input, IReadOnlyDictionary<string, string> replacements)
    {
        if (string.IsNullOrWhiteSpace(input) || replacements.IsNullOrEmpty())
        {
            return input;
        }

        foreach(var (key, value) in replacements)
        {
            input = input.Replace(key, value);
        }

        return input;
    }

    /// <summary>
    /// Removes all HTML tags from a string.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <returns>A <see cref="string"/>.</returns>
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
    
    /// <summary>
    /// Count the number of words in a string.
    /// </summary>
    /// <param name="text">The input text.</param>
    public static int CountWords(this string? text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return 0;
        }

        var punctuationCharacters = text.Where(char.IsPunctuation).Distinct().ToArray();
        var words = text.Split().Select(x => x.Trim(punctuationCharacters));
        return words.Count(x => !string.IsNullOrWhiteSpace(x));
    }

    /// <summary>
    /// Replace the last occurrence in a string.
    /// </summary>
    /// <param name="source">
    /// The source.
    /// </param>
    /// <param name="find">
    /// The find.
    /// </param>
    /// <param name="replace">
    /// The replacement value.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    [return: NotNullIfNotNull(nameof(source))]
    public static string? ReplaceLastOccurrence(this string? source, string find, string replace)
    {
        if (source == null)
        {
            return null;
        }

        var place = source.LastIndexOf(find, StringComparison.InvariantCultureIgnoreCase);

        if (place == -1)
        {
            return source;
        }

        return source.Remove(place, find.Length).Insert(place, replace);
    }

    /// <summary>
    /// Removes URLs from a string.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <returns></returns>
    [return: NotNullIfNotNull(nameof(input))]
    public static string? RemoveUrls(this string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return input;
        }

        return RemoveUrlsRegex().Replace(input, string.Empty).Replace("  ", " ").Trim();
    }

    [GeneratedRegex("[^\\w\\d\\s.,+-\\\\&#$%^(()\"'!*|\\[\\]]", RegexOptions.IgnoreCase)]
    private static partial Regex SanitizeRegex();

    [GeneratedRegex("<[^>]+>|&nbsp;")]
    private static partial Regex RemoveHtmlTagsRegex();

    [GeneratedRegex("\\s{2,}")]
    private static partial Regex NormalizeHtmlRegex();

    [GeneratedRegex(@"(?:https?:\/\/|www\.|\/\/)?\b[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}\b(?:[\/\?][^\s]*)?")]
    private static partial Regex RemoveUrlsRegex();
}