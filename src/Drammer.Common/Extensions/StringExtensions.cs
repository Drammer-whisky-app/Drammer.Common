using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;

namespace Drammer.Common.Extensions;

public static class StringExtensions
{
    [return: NotNullIfNotNull(nameof(emailAddress))]
    public static string? ObfuscateEmailAddress(this string? emailAddress)
    {
        if (string.IsNullOrWhiteSpace(emailAddress) || !emailAddress.Contains("@"))
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
}