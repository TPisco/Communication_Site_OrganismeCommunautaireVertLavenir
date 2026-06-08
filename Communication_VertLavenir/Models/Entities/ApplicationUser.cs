using System.ComponentModel.DataAnnotations;

namespace Communication_VertLavenir.Models
{
    public class ApplicationUser
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required, MaxLength(256)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [MaxLength(40)]
        public string? Phone { get; set; }

        public UserRole Role { get; set; } = UserRole.Member;

        public int? AddressId { get; set; }
        public Address? Address { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public ICollection<EventRegistration> Registrations { get; set; } = new List<EventRegistration>();
        public ICollection<Donation> Donations { get; set; } = new List<Donation>();

        public string FullName => $"{FirstName} {LastName}".Trim();
    }
}
