using System.ComponentModel.DataAnnotations;

namespace VertLavenir.Models;

public class Article
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Le titre est obligatoire.")]
    [StringLength(200, ErrorMessage = "Le titre ne peut pas dépasser 200 caractères.")]
    [Display(Name = "Titre")]
    public string Titre { get; set; } = string.Empty;

    [Required(ErrorMessage = "Le contenu est obligatoire.")]
    [Display(Name = "Contenu")]
    public string Contenu { get; set; } = string.Empty;

    [Display(Name = "Date de publication")]
    [DataType(DataType.DateTime)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
    public DateTime DatePublication { get; set; } = DateTime.Now;

    [StringLength(100)]
    [Display(Name = "Auteur")]
    public string? Auteur { get; set; }

    [StringLength(50)]
    [Display(Name = "Catégorie")]
    public string? Categorie { get; set; }

    [Display(Name = "Publié")]
    public bool EstPublie { get; set; } = false;

    [Display(Name = "URL de l'image")]
    [StringLength(500)]
    public string? ImageUrl { get; set; }
}
