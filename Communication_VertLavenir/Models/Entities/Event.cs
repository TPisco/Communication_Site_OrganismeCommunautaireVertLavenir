using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Communication_VertLavenir.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string TitleFr { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? TitleEn { get; set; }

        [MaxLength(200)]
        public string? TitleEs { get; set; }

        [Required]
        public string DescriptionFr { get; set; } = string.Empty;

        public string? DescriptionEn { get; set; }

        public string? DescriptionEs { get; set; }

        public DateTime StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }

        [MaxLength(250)]
        public string Location { get; set; } = string.Empty;

        public int Capacity { get; set; }

        public EventCategory Category { get; set; } = EventCategory.Autre;

        public bool IsPublic { get; set; } = true;
        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public ICollection<EventRegistration> Registrations { get; set; } = new List<EventRegistration>();

        [NotMapped]
        public string TitleLoc => Loc.Pick(TitleFr, TitleEn, TitleEs);

        [NotMapped]
        public string DescriptionLoc => Loc.Pick(DescriptionFr, DescriptionEn, DescriptionEs);
    }
}
