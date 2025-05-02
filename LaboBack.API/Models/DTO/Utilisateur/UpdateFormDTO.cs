namespace LaboBack.API.Models.DTO.Utilisateur
{
    public class UpdateFormDTO
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Mdp { get; set; }
        public string? Role { get; set; }
    }
}
