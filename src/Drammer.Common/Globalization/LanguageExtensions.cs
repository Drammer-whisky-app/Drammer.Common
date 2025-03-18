namespace Drammer.Common.Globalization;

public static class LanguageExtensions
{
    public static bool IsEnglish(this Language language) => language == Language.English;

    public static bool IsGerman(this Language language) => language == Language.German;

    public static bool IsDutch(this Language language) => language == Language.Dutch;
}