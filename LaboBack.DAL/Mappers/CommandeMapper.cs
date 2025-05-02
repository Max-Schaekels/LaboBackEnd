using LaboBack.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboBack.DAL.Mappers
{
    public class CommandeMapper
    {
        public static Commande ToDAL(IDataRecord record)
        {
            return new Commande
            {
                Id = (int)record["Id"],
                UtilisateurId = (int)record["UtilisateurId"],
                DateCommande = (DateTime)record["DateCommande"],
                StatutCommande = (string)record["StatutCommande"],

            };
        }

        public static Dictionary<string, object> ToDB(Commande commande)
        {
            Dictionary<string, object> commandeDictionnaire = new Dictionary<string, object>();
            commandeDictionnaire.Add("@UtilisateurId", commande.UtilisateurId);
            commandeDictionnaire.Add("@DateCommande", commande.DateCommande);
            commandeDictionnaire.Add("@StatutCommande", commande.StatutCommande);



            return commandeDictionnaire;

        }
    }
}
