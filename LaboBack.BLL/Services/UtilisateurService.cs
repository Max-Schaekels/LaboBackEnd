using LaboBack.BLL.Interfaces;
using LaboBack.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaboBack.BLL.Mappers;
using LaboBack.BLL.Models;
using System.Reflection.Metadata;

namespace LaboBack.BLL.Services
{
    public class UtilisateurService : IUtilisateurService
    {
        private readonly IUtilisateurRepository _repository;

        public UtilisateurService(IUtilisateurRepository repository)
        {
            _repository = repository;
        }

        // Connexion
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

        //Enregistrement
        public int Register(Utilisateur utilisateur)
        {
            //Hash du mdp
            string hashMdp = BCrypt.Net.BCrypt.HashPassword(utilisateur.Mdp);

            utilisateur.Mdp = hashMdp;

            int id = _repository.Create(utilisateur.BllToDal());
            return id;
        }

        //Récupération de tous les utilisateurs
        public IEnumerable<Utilisateur> GetAll()
        {
            return _repository
                .GetAll()
                .Select(u => u.DalToBll());
        }

        //Récupérer un utilisateur par son id
        public Utilisateur? GetById(int id)
        {
            var utilisateurDal = _repository.GetById(id);

            return utilisateurDal?.DalToBll();
        }

        //Récupérer un utilisateur par son email
        public Utilisateur? GetByEmail(string email)
        {
            var utilisateurDal = _repository.GetByEmail(email);

            return utilisateurDal?.DalToBll();
        }

        //Mise à jour d'un utilisateur
        public void Update(Utilisateur utilisateur)
        {
            // Rehash systématique du mot de passe fourni
            utilisateur.Mdp = BCrypt.Net.BCrypt.HashPassword(utilisateur.Mdp);

            // Récupérer l'utilisateur existant pour son ID
            var existingUser = _repository.GetByEmail(utilisateur.Email);
            if (existingUser == null)
            {
                throw new ArgumentException("Utilisateur introuvable.");
            }

            utilisateur.Id = existingUser.Id;

            // Mise à jour en base
            _repository.Update(utilisateur.BllToDal());
        }
    }
}
