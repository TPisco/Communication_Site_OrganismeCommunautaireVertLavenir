using System.ComponentModel.DataAnnotations;

namespace Communication_VertLavenir.Models
{
    public class Donation
    {
        public int Id { get; set; }

        [Range(1, 1000000)]
        public decimal Amount { get; set; }

        [MaxLength(10)]
        public string Currency { get; set; } = "CAD";

        [Required, MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required, MaxLength(256)]
        public string Email { get; set; } = string.Empty;

        [MaxLength(40)]
        public string? Phone { get; set; }

        [MaxLength(150)]
        public string? AddressLine1 { get; set; }

        [MaxLength(100)]
        public string? City { get; set; }

        [MaxLength(20)]
        public string? PostalCode { get; set; }

        [MaxLength(100)]
        public string? Province { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public DonationStatus Status { get; set; } = DonationStatus.Pending;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int? UserId { get; set; }
        public ApplicationUser? User { get; set; }

        public string DonorName => $"{FirstName} {LastName}".Trim();
    }
}
