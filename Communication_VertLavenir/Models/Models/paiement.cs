namespace Communication_VertLavenir.Models
{

    public enum typePaiement { paypal, cheques, carteCredit }
    public class paiement
    {
        public int id { get; set; }
        public int UtilisateurId { get; set; }   // FK
        public utilisateur utilisateur { get; set; }
        public typePaiement type {  get; set; }
        public int montant { get; set; }
    }
}
