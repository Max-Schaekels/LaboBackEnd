using System.ComponentModel.DataAnnotations;

namespace LaboBack.API.Models.DTO
{
    public class CaptchaDTO
    {
        [Required]
        public required string Token { get; set; }
    }
}
