using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaboBack.DAL.Interfaces;
using LaboBack.DAL.Mappers;
using LaboBack.DAL.Models;

namespace LaboBack.DAL.Repositories
{
    public class CommandeRepository : ICommandeRepository
    {
        private readonly Connection _connection;

        public CommandeRepository(Connection connection)
        {
            _connection = connection;
        }
        //Création d'une commande
        public int Create(Commande commande, List<Commande_Produit> lignes)
        {
            Command command = new Command("INSERT INTO Commande (UtilisateurId,DateCommande, StatutCommande) VALUES (@UtilisateurId,@DateCommande, @StatutCommande); SELECT SCOPE_IDENTITY();");
            var parametres = CommandeMapper.ToDB(commande);

            foreach (var param in parametres)
            {
                command.AddParameter(param.Key, param.Value);
            }

            var result = _connection.ExecuteScalar(command);
            int id = Convert.ToInt32(result);

            

            foreach (var ligne in lignes)
            {
                ligne.CommandeId = id;

                Command cmd = new Command("INSERT INTO MM_Commande_Produit (CommandeId,ProduitId,QuantiteCommandee) VALUES (@CommandeId,@ProduitId,@QuantiteCommandee); ");
                var param = Commande_ProduitMapper.ToDB(ligne);
                foreach (var par in param)
                {
                    cmd.AddParameter(par.Key, par.Value);
                }
                _connection.ExecuteNonQuery(cmd);
            }

            return id;

        }

        //Récupération d'une commande par son id
        public Commande? GetById(int id)
        {
            Command command = new Command("SELECT * FROM Commande WHERE Commande.Id = @Id");
            command.AddParameter("@Id", id);
            return _connection.ExecuteReader(command, CommandeMapper.ToDAL).FirstOrDefault();
        }

        //Récupération de la liste des commandes d'un utilisateur par son id
        public IEnumerable<Commande> GetByUtilisateurId(int utilisateurId)
        {
            Command command = new Command("SELECT * FROM Commande WHERE Commande.UtilisateurId = @UtilisateurId");
            command.AddParameter("@UtilisateurId", utilisateurId);
            return _connection.ExecuteReader(command, CommandeMapper.ToDAL);
        }

        //Récupération des lignes de la commande ( produit id, quantite commandee)
        public IEnumerable<Commande_Produit> GetLignesCommande(int commandeId)
        {
            Command command = new Command("SELECT * FROM MM_Commande_Produit as cp WHERE cp.CommandeId = @commandeId");
            command.AddParameter("@commandeId", commandeId);
            return _connection.ExecuteReader(command, Commande_ProduitMapper.ToDAL);
        }

        //Mise à jour du statut de la commande
        public void UpdateStatut(int commandeId, string nouveauStatut)
        {
            Command command = new Command("UPDATE Commande SET StatutCommande = @nouveauStatut WHERE Id = @commandeId");
            command.AddParameter("@nouveauStatut", nouveauStatut);
            command.AddParameter("@commandeId", commandeId);

            _connection.ExecuteNonQuery(command);

        }

        //Récupération des détails de la commande
        public IEnumerable<CommandeDetail> GetCommandeDetails(int commandeId)
        {
            Command command = new Command("SELECT cp.CommandeId, cp.ProduitId, p.Nom, p.Categorie, p.PrixHTVA, p.PrixTVAC, cp.QuantiteCommandee FROM MM_Commande_Produit cp JOIN Produit p ON cp.ProduitId = p.Id Where cp.CommandeId = @CommandeId");
            command.AddParameter("@CommandeId", commandeId);
            return _connection.ExecuteReader(command, CommandeDetailMapper.ToDAL);
        }
    }
}
