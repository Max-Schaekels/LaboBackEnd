using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bll = LaboBack.BLL.Models;
using dal = LaboBack.DAL.Models;

namespace LaboBack.BLL.Mappers
{
    public static class CommandMapper
    {
        public static bll.Commande DalToBll(this dal.Commande c)
        {
            return new bll.Commande
            {
                Id = c.Id,
                UtilisateurId = c.UtilisateurId,
                DateCommande = c.DateCommande,
                StatutCommande = c.StatutCommande


            };
        }

        public static dal.Commande BllToDal(this bll.Commande c)
        {
            return new dal.Commande
            {
                Id = c.Id,
                UtilisateurId = c.UtilisateurId,
                DateCommande = c.DateCommande,
                StatutCommande = c.StatutCommande

            };
        }
    }
}
