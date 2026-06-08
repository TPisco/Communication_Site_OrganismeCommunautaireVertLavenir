using System.ComponentModel.DataAnnotations;
using Communication_VertLavenir.Models;

namespace Communication_VertLavenir.ViewModels
{
    public class RegisterViewModel
    {
        [Required, Display(Name = "Prénom")]
        public string FirstName { get; set; } = string.Empty;

        [Required, Display(Name = "Nom")]
        public string LastName { get; set; } = string.Empty;

        [Required, EmailAddress, Display(Name = "Courriel")]
        public string Email { get; set; } = string.Empty;

        [Required, DataType(DataType.Password), MinLength(6), Display(Name = "Mot de passe")]
        public string Password { get; set; } = string.Empty;

        [Required, DataType(DataType.Password), Display(Name = "Confirmer le mot de passe")]
        [Compare(nameof(Password), ErrorMessage = "Les mots de passe ne correspondent pas.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }

    public class LoginViewModel
    {
        [Required, EmailAddress, Display(Name = "Courriel")]
        public string Email { get; set; } = string.Empty;

        [Required, DataType(DataType.Password), Display(Name = "Mot de passe")]
        public string Password { get; set; } = string.Empty;

        public bool RememberMe { get; set; } = true;

        public string? ReturnUrl { get; set; }
    }

    public class ProfileViewModel
    {
        [Required, Display(Name = "Prénom")]
        public string FirstName { get; set; } = string.Empty;

        [Required, Display(Name = "Nom")]
        public string LastName { get; set; } = string.Empty;

        [Required, EmailAddress, Display(Name = "Courriel")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Téléphone")]
        public string? Phone { get; set; }

        public List<EventRegistration> UpcomingRegistrations { get; set; } = new();
        public List<EventRegistration> PastRegistrations { get; set; } = new();
        public List<Donation> Donations { get; set; } = new();
    }

    public class ContactViewModel
    {
        [Required, Display(Name = "Nom complet")]
        public string Name { get; set; } = string.Empty;

        [Required, EmailAddress, Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Téléphone")]
        public string? Phone { get; set; }

        [Display(Name = "Sujet")]
        public string? Subject { get; set; }

        [Required, Display(Name = "Message")]
        public string Message { get; set; } = string.Empty;

        public string? OpeningHours { get; set; }
    }

    public class DonationViewModel
    {
        [Range(1, 1000000, ErrorMessage = "Veuillez choisir un montant valide.")]
        [Display(Name = "Montant")]
        public decimal Amount { get; set; }

        [Required, Display(Name = "Prénom")]
        public string FirstName { get; set; } = string.Empty;

        [Required, Display(Name = "Nom")]
        public string LastName { get; set; } = string.Empty;

        [Required, EmailAddress, Display(Name = "Courriel")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Téléphone")]
        public string? Phone { get; set; }

        [Display(Name = "Adresse")]
        public string? AddressLine1 { get; set; }

        [Display(Name = "Ville")]
        public string? City { get; set; }

        [Display(Name = "Code postal")]
        public string? PostalCode { get; set; }

        [Display(Name = "Province")]
        public string? Province { get; set; }

        public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.CarteCredit;

        [Display(Name = "Nom du titulaire")]
        public string? CardHolder { get; set; }

        [Display(Name = "Numéro de carte")]
        public string? CardNumber { get; set; }

        [Display(Name = "Expiration")]
        public string? CardExpiry { get; set; }

        [Display(Name = "CVV")]
        public string? CardCvv { get; set; }

        [Range(typeof(bool), "true", "true", ErrorMessage = "Vous devez accepter les conditions.")]
        public bool AcceptTerms { get; set; }
    }

    public class EventRegisterViewModel
    {
        public int EventId { get; set; }
        public string EventTitle { get; set; } = string.Empty;

        [Required, Display(Name = "Nom complet")]
        public string Name { get; set; } = string.Empty;

        [Required, EmailAddress, Display(Name = "Courriel")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Téléphone")]
        public string? Phone { get; set; }
    }
}
