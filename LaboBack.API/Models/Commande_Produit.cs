namespace LaboBack.API.Models
{
    public class Commande_Produit
    {
        public int CommandeId { get; set; }
        public int ProduitId { get; set; }
        public int QuantiteCommandee { get; set; }
    }
}
