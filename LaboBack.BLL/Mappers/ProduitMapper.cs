using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bll = LaboBack.BLL.Models;
using dal = LaboBack.DAL.Models;

namespace LaboBack.BLL.Mappers
{
    public static class ProduitMapper
    {
        public static bll.Produit DalToBll(this dal.Produit p)
        {
            return new bll.Produit
            {
                Id = p.Id,
                Nom = p.Nom,
                Description = p.Description,
                Categorie = p.Categorie,
                Quantite = p.Quantite,
                PrixHTVA = p.PrixHTVA,
                PrixTVAC = p.PrixTVAC
                
            };
        }

        public static dal.Produit BllToDal(this bll.Produit p)
        {
            return new dal.Produit
            {
                Id = p.Id,
                Nom = p.Nom,
                Description = p.Description,
                Categorie = p.Categorie,
                Quantite = p.Quantite,
                PrixHTVA = p.PrixHTVA,
                PrixTVAC = p.PrixTVAC
            };
        }
    }
}
