using System.ComponentModel.DataAnnotations;

namespace Communication_VertLavenir.Models
{
    public class EventRegistration
    {
        public int Id { get; set; }

        public int EventId { get; set; }
        public Event? Event { get; set; }

        public int? UserId { get; set; }
        public ApplicationUser? User { get; set; }

        [Required, MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        [Required, MaxLength(256)]
        public string Email { get; set; } = string.Empty;

        [MaxLength(40)]
        public string? Phone { get; set; }

        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;

        public RegistrationStatus Status { get; set; } = RegistrationStatus.Confirmed;
    }
}
