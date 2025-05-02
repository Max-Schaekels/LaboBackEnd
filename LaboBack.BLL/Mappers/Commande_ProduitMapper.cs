using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bll = LaboBack.BLL.Models;
using dal = LaboBack.DAL.Models;

namespace LaboBack.BLL.Mappers
{
    public static class Commande_ProduitMapper
    {
        public static bll.Commande_Produit CPDalToBll(this dal.Commande_Produit c)
        {
            return new bll.Commande_Produit
            {
                CommandeId = c.CommandeId,
                ProduitId = c.ProduitId,
                QuantiteCommandee = c.QuantiteCommandee

            };
        }

        public static dal.Commande_Produit CPBllToDal(this bll.Commande_Produit c)
        {
            return new dal.Commande_Produit
            {
                CommandeId = c.CommandeId,
                ProduitId = c.ProduitId,
                QuantiteCommandee = c.QuantiteCommandee

            };
        }
    }
}
