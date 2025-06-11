using LaboBack.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaboBack.DAL.Mappers;
using System.Runtime.InteropServices;
using LaboBack.DAL.Interfaces;

namespace LaboBack.DAL.Repositories
{
    public class UtilisateurRepository : IUtilisateurRepository
    {
        private readonly Connection _connection;

        public UtilisateurRepository(Connection connection)
        {
            _connection = connection;
        }

        //Récupération ensemble utilisateurs
        public IEnumerable<Utilisateur> GetAll()
        {
            Command command = new Command("SELECT * FROM Utilisateur");
            return _connection.ExecuteReader(command, UtilisateurMapper.ToDAL);
        }

        //Récupération utilisateur par l'id
        public Utilisateur? GetById(int id)
        {
            Command command = new Command("SELECT * FROM Utilisateur Where Utilisateur.Id = @Id");
            command.AddParameter("@Id", id);
            return _connection.ExecuteReader(command, UtilisateurMapper.ToDAL).FirstOrDefault();
        }

        //Récupération utilisateur par l'email
        public Utilisateur? GetByEmail(string email)
        {
            Command command = new Command("SELECT * FROM Utilisateur Where Utilisateur.Email = @Email");
            command.AddParameter("@Email", email);
            return _connection.ExecuteReader(command, UtilisateurMapper.ToDAL).FirstOrDefault();
        }

        //Récupération du mdp
        public string? GetPassword(string email)
        {
            Command command = new Command("SELECT MdpHash FROM Utilisateur Where Utilisateur.Email = @Email");
            command.AddParameter("@Email", email);
            object result = _connection.ExecuteScalar(command);
            return result == null || result == DBNull.Value ? null : result.ToString();
        }

        //Création d'un utilisateur
        public int Create(Utilisateur utilisateur)
        {
            Command command = new Command("INSERT INTO UTILISATEUR (Nom,Prenom,Email, MdpHash,Role) VALUES (@Nom,@Prenom, @Email, @Mdp,@Role); SELECT SCOPE_IDENTITY();");
            var parametres = UtilisateurMapper.ToDB(utilisateur); // utilisation du mappeur pour récupérer dictionnaire des paramètre 

            //Ajout des paramètres
            foreach (var param in parametres)
            {
                command.AddParameter(param.Key, param.Value);
            }
            var result = _connection.ExecuteScalar(command);
            int id = Convert.ToInt32(result);
            return id;
        }

        //Mise à jour d'un utilisateur
        public bool Update(Utilisateur utilisateur)
        {
            Command command = new Command("UPDATE Utilisateur SET Nom = @Nom," +
                                                            " Prenom = @Prenom," +
                                                            " MdpHash = @Mdp," +
                                                            " Role = @Role" +
                                                            " WHERE Id = @id");
            var parametres = UtilisateurMapper.UpdateToDB(utilisateur);  

            foreach (var param in parametres)
            {
                command.AddParameter(param.Key, param.Value);
            }
            command.AddParameter("@id", utilisateur.Id);

            int rows = _connection.ExecuteNonQuery(command);
            return rows > 0;
        }
    }
}
