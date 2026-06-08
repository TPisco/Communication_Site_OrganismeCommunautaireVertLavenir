using Communication_VertLavenir.Models;

namespace Communication_VertLavenir.Areas.Admin.Models
{
    public class MonthlyPoint
    {
        public string Label { get; set; } = string.Empty;
        public decimal Sum { get; set; }
        public int Count { get; set; }
    }

    public class CategoryPoint
    {
        public string Label { get; set; } = string.Empty;
        public int Count { get; set; }
    }

    public class AdminDashboardViewModel
    {
        public int TotalMembers { get; set; }
        public int TotalEvents { get; set; }
        public int TotalRegistrations { get; set; }
        public int TotalDonations { get; set; }
        public decimal TotalDonated { get; set; }
        public int NewContactMessages { get; set; }

        public List<MonthlyPoint> DonationsPerMonth { get; set; } = new();
        public List<MonthlyPoint> NewMembersPerMonth { get; set; } = new();
        public List<CategoryPoint> TopCategories { get; set; } = new();

        // Participation rate across events
        public int CapacityTotal { get; set; }
        public int ConfirmedTotal { get; set; }
        public double ParticipationRate => CapacityTotal > 0 ? Math.Round((double)ConfirmedTotal / CapacityTotal * 100, 1) : 0;
    }

    public class DonationFilterViewModel
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public decimal? MinAmount { get; set; }
        public DonationStatus? Status { get; set; }
        public PaymentMethod? Method { get; set; }

        public List<Donation> Results { get; set; } = new();
        public decimal Total { get; set; }
    }

    public class MemberDetailViewModel
    {
        public ApplicationUser User { get; set; } = null!;
        public List<EventRegistration> Registrations { get; set; } = new();
        public List<Donation> Donations { get; set; } = new();
    }

    public class EventRegistrationsViewModel
    {
        public Event Event { get; set; } = null!;
        public List<EventRegistration> Registrations { get; set; } = new();
    }
}
