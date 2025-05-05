using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bll = LaboBack.BLL.Models;
using dal = LaboBack.DAL.Models;

namespace LaboBack.BLL.Mappers
{
    public static class CommandeDetailMapper
    {
        public static bll.CommandeDetail DalToBll(this dal.CommandeDetail p)
        {
            return new bll.CommandeDetail
            {
                CommandeId = p.CommandeId,
                ProduitId = p.ProduitId,
                Nom = p.Nom,
                Categorie = p.Categorie,
                PrixHTVA = p.PrixHTVA,
                PrixTVAC = p.PrixTVAC,
                QuantiteCommandee = p.QuantiteCommandee

            };
        }

        public static dal.CommandeDetail BllToDal(this bll.CommandeDetail p)
        {
            return new dal.CommandeDetail
            {
                CommandeId = p.CommandeId,
                ProduitId = p.ProduitId,
                Nom = p.Nom,
                Categorie = p.Categorie,
                PrixHTVA = p.PrixHTVA,
                PrixTVAC = p.PrixTVAC,
                QuantiteCommandee = p.QuantiteCommandee
            };
        }
    }
}
