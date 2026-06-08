using System.Globalization;

namespace Communication_VertLavenir.Services
{
    public static class LocalizationHelper
    {
        // True when the current UI culture is English.
        public static bool IsEnglish =>
            CultureInfo.CurrentUICulture.TwoLetterISOLanguageName
                .Equals("en", StringComparison.OrdinalIgnoreCase);

        public static string Money(decimal amount) =>
            amount.ToString("C0", CultureInfo.CurrentUICulture.Name == "en-CA"
                ? new CultureInfo("en-CA")
                : new CultureInfo("fr-CA"));
    }
}
