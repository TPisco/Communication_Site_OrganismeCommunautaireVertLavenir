using System.ComponentModel.DataAnnotations;

namespace VertLavenir.Models;

public class Membre
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Le prénom est obligatoire.")]
    [StringLength(100)]
    [Display(Name = "Prénom")]
    public string Prenom { get; set; } = string.Empty;

    [Required(ErrorMessage = "Le nom est obligatoire.")]
    [StringLength(100)]
    [Display(Name = "Nom")]
    public string Nom { get; set; } = string.Empty;

    [Required(ErrorMessage = "Le courriel est obligatoire.")]
    [EmailAddress(ErrorMessage = "Adresse courriel invalide.")]
    [StringLength(200)]
    [Display(Name = "Courriel")]
    public string Courriel { get; set; } = string.Empty;

    [Phone(ErrorMessage = "Numéro de téléphone invalide.")]
    [StringLength(20)]
    [Display(Name = "Téléphone")]
    public string? Telephone { get; set; }

    [Display(Name = "Date d'adhésion")]
    [DataType(DataType.Date)]
    public DateTime DateAdhesion { get; set; } = DateTime.Today;

    [Display(Name = "Actif")]
    public bool EstActif { get; set; } = true;

    [StringLength(50)]
    [Display(Name = "Rôle")]
    public string? Role { get; set; }

    [Display(Name = "Nom complet")]
    public string NomComplet => $"{Prenom} {Nom}";
}
