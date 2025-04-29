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

        public IEnumerable<Utilisateur> GetAll()
        {
            Command command = new Command("SELECT * FROM Utilisateur");
            return _connection.ExecuteReader(command, UtilisateurMapper.ToDAL);
        }

        public Utilisateur? GetById(int id)
        {
            Command command = new Command("SELECT * FROM Utilisateur Where Utilisateur.Id = @Id");
            command.AddParameter("@Id", id);
            return _connection.ExecuteReader(command, UtilisateurMapper.ToDAL).FirstOrDefault();
        }


        public Utilisateur? GetByEmail(string email)
        {
            Command command = new Command("SELECT * FROM Utilisateur Where Utilisateur.Email = @Email");
            command.AddParameter("@Email", email);
            return _connection.ExecuteReader(command, UtilisateurMapper.ToDAL).FirstOrDefault();
        }

        public string? GetPassword(string email)
        {
            Command command = new Command("SELECT MdpHash FROM Utilisateur Where Utilisateur.Email = @Email");
            command.AddParameter("@Email", email);
            object result = _connection.ExecuteScalar(command);
            return result == null || result == DBNull.Value ? null : result.ToString();
        }

        public int Create(Utilisateur utilisateur)
        {
            Command command = new Command("INSERT INTO UTILISATEUR (Nom,Email, MdpHash,Role) VALUES (@Nom, @Email, @Mdp,@Role); SELECT SCOPE_IDENTITY();");
            var parametres = UtilisateurMapper.ToDB(utilisateur); // utilisation du mappeur pour récupérer dictionnaire des paramètre 

            foreach (var param in parametres)
            {
                command.AddParameter(param.Key, param.Value);
            }
            var result = _connection.ExecuteScalar(command);
            int id = Convert.ToInt32(result);
            return id;
        }
    }
}
