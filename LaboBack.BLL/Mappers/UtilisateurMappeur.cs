using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bll = LaboBack.BLL.Models;
using dal = LaboBack.DAL.Models;

namespace LaboBack.BLL.Mappers
{
    public static class UtilisateurMappeur
    {
        public static bll.Utilisateur DalToBll(this dal.Utilisateur u)
        {
            return new bll.Utilisateur
            {
                Id = u.Id,
                Nom = u.Nom,
                Prenom = u.Prenom,
                Email = u.Email,
                Role = u.Role
                // On ne retourne jamais le mot de passe dans la couche BLL
            };
        }

        public static dal.Utilisateur BllToDal(this bll.Utilisateur u)
        {
            return new dal.Utilisateur
            {
                Id = u.Id,
                Nom = u.Nom,
                Prenom = u.Prenom,
                Email = u.Email,
                Mdp = u.Mdp,
                Role = u.Role
            };
        }
    }
}
