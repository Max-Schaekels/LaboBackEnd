using System.ComponentModel.DataAnnotations;

namespace LaboBack.API.Models.DTO.Utilisateur
{
    public class LoginFormDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Mdp { get; set; }
    }
}
