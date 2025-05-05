using LaboBack.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboBack.DAL.Interfaces
{
    public interface ICommandeRepository
    {
        int Create(Commande commande, List<Commande_Produit> lignes);
        Commande? GetById(int id);
        IEnumerable<Commande> GetByUtilisateurId(int utilisateurId);
        IEnumerable<Commande_Produit> GetLignesCommande(int commandeId);
        void UpdateStatut(int commandeId, string nouveauStatut);
        IEnumerable<CommandeDetail> GetCommandeDetails(int commandeId);
    }
}
