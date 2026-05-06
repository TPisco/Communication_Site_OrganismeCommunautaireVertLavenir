namespace Communication_VertLavenir.Models
{
    public enum typeEvent { Collecte, Activité, Bénévolat }
    public class evenement
    {
        public int id { get; set; }
        public DateTime date { get; set; }
        public string nom { get; set; }
        public string description { get; set; }
        public typeEvent type { get; set; }
    }
}
