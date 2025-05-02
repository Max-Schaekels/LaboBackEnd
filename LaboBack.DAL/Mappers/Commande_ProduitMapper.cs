using LaboBack.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboBack.DAL.Mappers
{
    public class Commande_ProduitMapper
    {
        public static Commande_Produit ToDAL(IDataRecord record)
        {
            return new Commande_Produit
            {
                CommandeId = (int)record["CommandeId"],
                ProduitId = (int)record["ProduitId"],
                QuantiteCommandee = (int)record["QuantiteCommandee"],


            };
        }

        public static Dictionary<string, object> ToDB(Commande_Produit commande_produit)
        {
            Dictionary<string, object> cpDictionnaire = new Dictionary<string, object>();
            cpDictionnaire.Add("@CommandeId", commande_produit.CommandeId);
            cpDictionnaire.Add("@ProduitId", commande_produit.ProduitId);
            cpDictionnaire.Add("@QuantiteCommandee", commande_produit.QuantiteCommandee);



            return cpDictionnaire;

        }
    }
}
