using System.ComponentModel.DataAnnotations;

namespace Communication_VertLavenir.Models
{
    // Clé/valeur générique : coordonnées, heures d'ouverture, liens sociaux, textes de pied de page, mission...
    public class SiteSetting
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Key { get; set; } = string.Empty;

        public string? Value { get; set; }

        [MaxLength(250)]
        public string? Description { get; set; }
    }

    public static class SiteSettingKeys
    {
        public const string MissionFr = "Mission.Fr";
        public const string MissionEn = "Mission.En";
        public const string MissionEs = "Mission.Es";
        public const string ContactEmail = "Contact.Email";
        public const string ContactPhone = "Contact.Phone";
        public const string ContactAddress = "Contact.Address";
        public const string OpeningHoursFr = "OpeningHours.Fr";
        public const string OpeningHoursEn = "OpeningHours.En";
        public const string OpeningHoursEs = "OpeningHours.Es";
        public const string SocialFacebook = "Social.Facebook";
        public const string SocialInstagram = "Social.Instagram";
        public const string SocialLinkedIn = "Social.LinkedIn";
    }
}
