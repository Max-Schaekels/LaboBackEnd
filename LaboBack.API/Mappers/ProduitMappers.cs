using bll = LaboBack.BLL.Models;
using api = LaboBack.API.Models;
using LaboBack.API.Models.DTO.Produit;


namespace LaboBack.API.Mappers
{
    public static class ProduitMappers
    {
        public static bll.Produit ApiToBll(this ProduitFormDTO form)
        {
            return new bll.Produit
            {
                Nom = form.Nom,
                Description = form.Description,
                Categorie = form.Categorie,
                Quantite = form.Quantite,
                PrixHTVA = form.PrixHTVA,
                PrixTVAC = form.PrixTVAC
            };
        }

        public static api.Produit BllToApi(this bll.Produit form)
        {
            return new api.Produit
            {
                Id = form.Id,
                Nom = form.Nom,
                Description = form.Description,
                Categorie = form.Categorie,
                Quantite = form.Quantite,
                PrixHTVA = form.PrixHTVA,
                PrixTVAC = form.PrixTVAC
            };
        }

        public static bll.Produit ApiToBll(this UpdateProduitFormDTO form)
        {
            return new bll.Produit
            {
                Nom = form.Nom,
                Description = form.Description,
                Categorie = form.Categorie,
                Quantite = form.Quantite,
                PrixHTVA = form.PrixHTVA,
                PrixTVAC = form.PrixTVAC
            };
        }
    }
}
