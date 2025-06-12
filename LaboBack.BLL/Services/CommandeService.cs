using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaboBack.BLL.Interfaces;
using LaboBack.DAL.Interfaces;
using LaboBack.BLL.Mappers;
using LaboBack.BLL.Models;
using LaboBack.DAL.Models;
using Commande = LaboBack.BLL.Models.Commande;
using Commande_Produit = LaboBack.BLL.Models.Commande_Produit;
using CommandeDetail = LaboBack.BLL.Models.CommandeDetail;
using LaboBack.DAL.Repositories;

namespace LaboBack.BLL.Services
{
    public class CommandeService : ICommandeService
    {
        private readonly ICommandeRepository _repository;

        public CommandeService(ICommandeRepository repository)
        {
            _repository = repository;
        }

        //Creation d'une commande
        public int Create(Commande commande, List<Commande_Produit> lignes)
        {
            var lignesDal = lignes.Select(l => l.CPBllToDal()).ToList();
            int id = _repository.Create(commande.BllToDal(), lignesDal);
            return id;
        }

        //Récupération par l'id
        public Commande? GetById(int id)
        {
            var comDal = _repository.GetById(id);
            return comDal?.DalToBll();
        }

        //Récupération des commandes d'un utilisateur
        public IEnumerable<Commande> GetByUtilisateurId(int utilisateurId)
        {
            return _repository.GetByUtilisateurId(utilisateurId).Select(c => c.DalToBll());
        }

        //Récupération des lignes d'une commande (quantité commandée,id produit)
        public IEnumerable<Commande_Produit> GetLignesCommande(int commandeId)
        {
            return _repository.GetLignesCommande(commandeId).Select(c => c.CPDalToBll());
        }

        //Mise à jour du status
        public void UpdateStatut(int commandeId, string nouveauStatut)
        {
            _repository.UpdateStatut(commandeId, nouveauStatut);
        }

        //Detail d'une commande (nom, catégorie, prix et quantite commandee)
        public IEnumerable<CommandeDetail> GetCommandeDetails(int commandeId)
        {
            return _repository.GetCommandeDetails(commandeId).Select(c => c.DalToBll());
        }

        //Récupération de toutes les commandes pour l'admin 
        public IEnumerable<Commande> GetAll()
        {
            return _repository.GetAll().Select(c => c.DalToBll());
        }
    }
}
