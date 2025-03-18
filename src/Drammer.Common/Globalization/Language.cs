namespace Drammer.Common.Globalization;

public record struct Language
{
    private const string LanguageDutch = "nl";
    private const string LanguageGerman = "de";
    private const string LanguageEnglish = "en";

    public string Value { get; private init; }

    public static Language English => new() { Value = LanguageEnglish };
    public static Language Dutch => new() { Value = LanguageDutch };
    public static Language German => new() { Value = LanguageGerman };

    /// <summary>
    /// Constructs a new instance of <see cref="Language"/> based on the input value.
    /// When the value is not found, the default value is <see cref="English"/>.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static Language ToLanguageOrDefault(string? value)
    {
        if (value == null)
        {
            return English;
        }

        var v = value.ToLowerInvariant().Trim();
        return v switch
        {
            LanguageEnglish => English,
            LanguageDutch => Dutch,
            LanguageGerman => German,
            _ => English
        };
    }

    /// <summary>
    /// Constructs a new instance of <see cref="Language"/> based on the input value.
    /// When the value is not found, the default value is null.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static Language? ToLanguageOrNull(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        var v = value.ToLowerInvariant().Trim();
        return v switch
        {
            LanguageEnglish => English,
            LanguageDutch => Dutch,
            LanguageGerman => German,
            _ => null
        };
    }
}