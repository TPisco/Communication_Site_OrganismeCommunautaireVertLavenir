namespace Communication_VertLavenir.Models
{
    public enum UserRole
    {
        Admin,
        Staff,
        Member
    }

    public enum EventCategory
    {
        Jardinage,
        Collecte,
        Atelier,
        Conference,
        Plantation,
        Nettoyage,
        Autre
    }

    public enum RegistrationStatus
    {
        Confirmed,
        Waitlisted,
        Cancelled
    }

    public enum PaymentMethod
    {
        CarteCredit,
        PayPal,
        Cheque
    }

    public enum DonationStatus
    {
        Pending,
        Completed,
        Failed
    }

    public enum ContactStatus
    {
        New,
        Read,
        Archived
    }
}
