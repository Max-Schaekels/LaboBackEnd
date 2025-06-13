using System.Text.Json.Serialization;

namespace LaboBack.API.Models.DTO.Commande
{
    public class LigneCommandeFormDTO
    {
        public int ProduitId { get; set; }
        [JsonPropertyName("quantite")]
        public int QuantiteCommandee { get; set; }
    }
}
