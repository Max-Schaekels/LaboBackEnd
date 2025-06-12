using LaboBack.DAL.Interfaces;
using LaboBack.DAL.Mappers;
using LaboBack.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboBack.DAL.Repositories
{
    public class ProduitRepository : IProduitRepository
    {
        private readonly Connection _connection;

        public ProduitRepository(Connection connection)
        {
            _connection = connection;
        }

        //Creation d'un produit
        public int Create(Produit produit)
        {
            Command command = new Command("INSERT INTO Produit (Nom,Description,Categorie, Quantite,PrixHTVA, PrixTVAC) VALUES (@Nom,@Description,@Categorie, @Quantite, @PrixHTVA,@PrixTVAC); SELECT SCOPE_IDENTITY();");
            var parametres = ProduitMapper.ToDB(produit); 

            foreach (var param in parametres)
            {
                command.AddParameter(param.Key, param.Value);
            }
            var result = _connection.ExecuteScalar(command);
            int id = Convert.ToInt32(result);
            return id;
        }


        //Récupéaration de tous les produits
        public IEnumerable<Produit> GetAll()
        {
            Command command = new Command("SELECT * FROM Produit");
            return _connection.ExecuteReader(command, ProduitMapper.ToDAL);
        }

        //Récupération par un id
        public Produit? GetById(int id)
        {
            Command command = new Command("SELECT * FROM Produit Where Produit.Id = @Id");
            command.AddParameter("@Id", id);
            return _connection.ExecuteReader(command, ProduitMapper.ToDAL).FirstOrDefault();
        }

        //Mise à jour du produit
        public void Update(Produit produit)
        {
            Command command = new Command("UPDATE Produit SET Nom = @Nom," +
                                                " Description = @Description," +
                                                " Categorie = @Categorie," +
                                                " Quantite = @Quantite," +
                                                " PrixHTVA = @PrixHTVA," +
                                                " PrixTVAC = @PrixTVAC" +
                                                " WHERE Id = @id");
            var parametres = ProduitMapper.ToDB(produit);

            foreach (var param in parametres)
            {
                command.AddParameter(param.Key, param.Value);
            }
            command.AddParameter("@id", produit.Id);

            _connection.ExecuteNonQuery(command);
        }

        //Tri produit par catégorie
        public IEnumerable<Produit> GetByCategorie(string categorie)
        {
            Command command = new Command("SELECT * FROM Produit WHERE Categorie = @categorie ORDER BY Categorie");

            command.AddParameter("@categorie", categorie);

            return _connection.ExecuteReader(command, ProduitMapper.ToDAL);
        }

        //Suppresion Produit
        public void Delete(int id)
        {
            Command command = new Command("DELETE FROM Produit WHERE Id = @id");

            command.AddParameter("@id", id);

            _connection.ExecuteNonQuery(command);
        }

        public IEnumerable<string> GetCategories()
        {
            Command command = new Command("SELECT DISTINCT Categorie FROM Produit ORDER BY Categorie");
            return _connection.ExecuteReader(command, reader => reader.GetString("Categorie"));
        }
    }
}
