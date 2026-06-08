using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Communication_VertLavenir.Models
{
    // Programme / action clé (Reforestation, Recyclage, Éducation, Biodiversité, ...)
    public class KeyAction
    {
        public int Id { get; set; }

        [Required, MaxLength(150)]
        public string TitleFr { get; set; } = string.Empty;

        [MaxLength(150)]
        public string? TitleEn { get; set; }

        [MaxLength(150)]
        public string? TitleEs { get; set; }

        [Required]
        public string DescriptionFr { get; set; } = string.Empty;

        public string? DescriptionEn { get; set; }

        public string? DescriptionEs { get; set; }

        [MaxLength(50)]
        public string? Icon { get; set; }

        public int DisplayOrder { get; set; }

        [NotMapped]
        public string TitleLoc => Loc.Pick(TitleFr, TitleEn, TitleEs);

        [NotMapped]
        public string DescriptionLoc => Loc.Pick(DescriptionFr, DescriptionEn, DescriptionEs);
    }
}
