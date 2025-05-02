using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace LaboBack.API.Models.DTO.Utilisateur
{
    public class RegisterFormDTO
    {
        [Required]
        public string Nom { get; set; }
        [Required]
        public string Prenom { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Mdp { get; set; }
        public string? Role { get; set; }
    }
}
