namespace LaboBack.API.Models.DTO.Commande
{
    public class CommandeDTO
    {
        public int Id { get; set; }
        public int UtilisateurId { get; set; }
        public DateTime Date { get; set; }
        public string Statut { get; set; }

    }
}
