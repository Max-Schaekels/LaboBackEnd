namespace LaboBack.API.Models
{
    public class Commande
    {
        public int Id { get; set; }
        public int UtilisateurId { get; set; }
        public DateTime DateCommande { get; set; }
        public string StatutCommande { get; set; }
    }
}
