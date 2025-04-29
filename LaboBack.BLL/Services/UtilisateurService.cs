using LaboBack.BLL.Interfaces;
using LaboBack.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaboBack.BLL.Mappers;
using LaboBack.BLL.Models;

namespace LaboBack.BLL.Services
{
    public class UtilisateurService : IUtilisateurService
    {
        private readonly IUtilisateurRepository _repository;

        public UtilisateurService(IUtilisateurRepository repository)
        {
            _repository = repository;
        }

        public Utilisateur Login(string email, string mdp)
        {
            try
            {
                // comparé le mot de passe 
                string hashmdp = _repository.GetPassword(email);

                if (string.IsNullOrEmpty(hashmdp))
                {
                    throw new ArgumentException("Email incorrect.");
                }

                // récupère une personne par rapport a sont email et de vérifier que les mot de passe sont identique.
                if (!BCrypt.Net.BCrypt.Verify(mdp, hashmdp))
                {
                    throw new ArgumentException("Mot de passe incorect.");
                }

                // Conversion vers la dal
                var utilisateur = _repository.GetByEmail(email);

                if (utilisateur == null)
                {
                    throw new ArgumentException("Utilisateur introuvable.");
                }

                return utilisateur.DalToBll();
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentException("Email incorrect.");
            }


        }

        public int Register(Utilisateur utilisateur)
        {
            string hashMdp = BCrypt.Net.BCrypt.HashPassword(utilisateur.Mdp);

            utilisateur.Mdp = hashMdp;

            int id = _repository.Create(utilisateur.BllToDal());
            return id;
        }
    }
}
