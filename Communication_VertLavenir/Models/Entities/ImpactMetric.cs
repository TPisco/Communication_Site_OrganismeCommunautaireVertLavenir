using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Communication_VertLavenir.Models
{
    public class ImpactMetric
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Key { get; set; } = string.Empty;

        [Required, MaxLength(150)]
        public string LabelFr { get; set; } = string.Empty;

        [MaxLength(150)]
        public string? LabelEn { get; set; }

        [MaxLength(150)]
        public string? LabelEs { get; set; }

        [Required, MaxLength(50)]
        public string Value { get; set; } = string.Empty;

        [MaxLength(30)]
        public string? Unit { get; set; }

        [MaxLength(50)]
        public string? Icon { get; set; }

        public int DisplayOrder { get; set; }

        [NotMapped]
        public string LabelLoc => Loc.Pick(LabelFr, LabelEn, LabelEs);
    }
}
