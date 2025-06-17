using System.Text.Json.Serialization;

namespace LaboBack.API.Models
{
    public class CommandeDetail
    {
        public int CommandeId { get; set; }
        public int ProduitId { get; set; }
        public string Nom { get; set; }
        public string Categorie { get; set; }
        public decimal PrixHTVA { get; set; }
        public decimal PrixTVAC { get; set; }
        [JsonPropertyName("quantite")]
        public int QuantiteCommandee { get; set; }
    }
}
