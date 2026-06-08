using System.Globalization;

namespace Communication_VertLavenir.Models
{
    // Picks the right string for the current UI culture, falling back to French (the source language).
    public static class Loc
    {
        public static string Pick(string fr, string? en, string? es)
        {
            var lang = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            return lang switch
            {
                "en" => string.IsNullOrWhiteSpace(en) ? fr : en!,
                "es" => string.IsNullOrWhiteSpace(es) ? fr : es!,
                _ => fr
            };
        }
    }
}
