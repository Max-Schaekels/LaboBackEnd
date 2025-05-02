using LaboBack.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboBack.DAL.Mappers
{
    public class UtilisateurMapper
    {
        public static Utilisateur ToDAL(IDataRecord record)
        {
            return new Utilisateur 
            { 
                Id = (int)record["Id"], 
                Nom = (string)record["Nom"],
                Prenom = (string)record["Prenom"],
                Email = (string)record["Email"], 
                Mdp = (string)record["MdpHash"], 
                Role = (record["Role"]== DBNull.Value) ? null : (string)record["Role"]
            };
        }

        public static Dictionary<string, object> ToDB(Utilisateur utilisateur)
        {
            Dictionary<string, object> utilisateurDictionnaire = new Dictionary<string, object>();
            utilisateurDictionnaire.Add("@Nom", utilisateur.Nom);
            utilisateurDictionnaire.Add("@Prenom", utilisateur.Prenom);
            utilisateurDictionnaire.Add("@Email", utilisateur.Email);
            utilisateurDictionnaire.Add("@Mdp", utilisateur.Mdp);
            utilisateurDictionnaire.Add("@Role", utilisateur.Role == null ? DBNull.Value : utilisateur.Role);

            return utilisateurDictionnaire;

        }

        public static Dictionary<string, object> UpdateToDB(Utilisateur utilisateur)
        {
            Dictionary<string, object> utilisateurDictionnaire = new Dictionary<string, object>();
            utilisateurDictionnaire.Add("@Nom", utilisateur.Nom);
            utilisateurDictionnaire.Add("@Prenom", utilisateur.Prenom);
            utilisateurDictionnaire.Add("@Mdp", utilisateur.Mdp);
            utilisateurDictionnaire.Add("@Role", utilisateur.Role == null ? DBNull.Value : utilisateur.Role);

            return utilisateurDictionnaire;

        }
    }
}
