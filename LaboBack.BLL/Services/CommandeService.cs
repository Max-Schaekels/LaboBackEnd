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

namespace LaboBack.BLL.Services
{
    public class CommandeService : ICommandeService
    {
        private readonly ICommandeRepository _repository;

        public CommandeService(ICommandeRepository repository)
        {
            _repository = repository;
        }
        public int Create(Commande commande, List<Commande_Produit> lignes)
        {
            var lignesDal = lignes.Select(l => l.CPBllToDal()).ToList();
            int id = _repository.Create(commande.BllToDal(), lignesDal);
            return id;
        }

        public Commande? GetById(int id)
        {
            var comDal = _repository.GetById(id);
            return comDal?.DalToBll();
        }

        public IEnumerable<Commande> GetByUtilisateurId(int utilisateurId)
        {
            return _repository.GetByUtilisateurId(utilisateurId).Select(c => c.DalToBll());
        }

        public IEnumerable<Commande_Produit> GetLignesCommande(int commandeId)
        {
            return _repository.GetLignesCommande(commandeId).Select(c => c.CPDalToBll());
        }

        public void UpdateStatut(int commandeId, string nouveauStatut)
        {
            _repository.UpdateStatut(commandeId, nouveauStatut);
        }
    }
}
