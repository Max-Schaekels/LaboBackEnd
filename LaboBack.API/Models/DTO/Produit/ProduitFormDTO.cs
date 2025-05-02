using System.ComponentModel.DataAnnotations;

namespace LaboBack.API.Models.DTO.Produit
{
    public class ProduitFormDTO
    {
        [Required]
        public string Nom { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Categorie { get; set; }
        [Required]
        public int Quantite { get; set; }
        [Required]
        public decimal PrixHTVA { get; set; }
        [Required]
        public decimal PrixTVAC { get; set; }
    }
}
