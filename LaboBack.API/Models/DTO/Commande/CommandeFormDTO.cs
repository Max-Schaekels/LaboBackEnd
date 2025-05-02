namespace LaboBack.API.Models.DTO.Commande
{
    public class CommandeFormDTO
    {
        public int UtilisateurId { get; set; }
        public List<LigneCommandeFormDTO> Lignes { get; set; }
    }
}
