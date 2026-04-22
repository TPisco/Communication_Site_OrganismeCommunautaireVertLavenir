namespace Communication_VertLavenir.Models
{

    public enum role { admin, chefBenevol, benevol, donneur, utilisateur }

    public class utilisateur
    {
        public int id { get; set; }
        public string nom { get; set; }
        public string prenom {  get; set; }
        public role role { get; set; }
        public string email { get; set; }
        public string numero { get; set; }
        public int AddressId { get; set; }   // FK
        public adresse Address { get; set; }
    }
}
