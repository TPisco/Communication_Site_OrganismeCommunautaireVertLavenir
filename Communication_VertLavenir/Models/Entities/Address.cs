using System.ComponentModel.DataAnnotations;

namespace Communication_VertLavenir.Models
{
    public class Address
    {
        public int Id { get; set; }

        [MaxLength(150)]
        public string? Line1 { get; set; }

        [MaxLength(100)]
        public string? City { get; set; }

        [MaxLength(20)]
        public string? PostalCode { get; set; }

        [MaxLength(100)]
        public string? Province { get; set; }
    }
}
