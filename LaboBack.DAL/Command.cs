using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboBack.DAL
{
    public class Command
    {
        // Le texte SQL ou le nom de la procédure
        public string Query { get; }

        // Les paramètres SQL (clé = nom du paramètre, valeur = valeur du paramètre)
        public Dictionary<string, object> Parameters { get; } = new Dictionary<string, object>();

        // Le type de commande : Text (requête) ou StoredProcedure
        public CommandType CommandType { get; }

        // Constructeur : on donne la requête et le type (par défaut Text)
        public Command(string query, CommandType commandType = CommandType.Text)
        {
            Query = query;
            CommandType = commandType;
        }

        // Méthode pour ajouter un paramètre à la commande
        public void AddParameter(string name, object value)
        {
            if (!Parameters.ContainsKey(name))
            {
                Parameters.Add(name, value ?? DBNull.Value); // Ajoute NULL SQL si la valeur est null
            }
        }
    }
}
