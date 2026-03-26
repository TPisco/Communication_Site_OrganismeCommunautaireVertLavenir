using System.ComponentModel.DataAnnotations;

namespace VertLavenir.Models;

public class Evenement
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Le titre est obligatoire.")]
    [StringLength(200, ErrorMessage = "Le titre ne peut pas dépasser 200 caractères.")]
    [Display(Name = "Titre")]
    public string Titre { get; set; } = string.Empty;

    [Required(ErrorMessage = "La description est obligatoire.")]
    [Display(Name = "Description")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "La date de début est obligatoire.")]
    [Display(Name = "Date de début")]
    [DataType(DataType.DateTime)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
    public DateTime DateDebut { get; set; } = DateTime.Now;

    [Display(Name = "Date de fin")]
    [DataType(DataType.DateTime)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
    public DateTime? DateFin { get; set; }

    [Required(ErrorMessage = "Le lieu est obligatoire.")]
    [StringLength(200)]
    [Display(Name = "Lieu")]
    public string Lieu { get; set; } = string.Empty;

    [Range(1, 10000)]
    [Display(Name = "Nombre max de participants")]
    public int? NombreMaxParticipants { get; set; }

    [Display(Name = "Actif")]
    public bool EstActif { get; set; } = true;
}
