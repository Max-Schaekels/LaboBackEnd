using LaboBack.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboBack.DAL.Mappers
{
    public class ProduitMapper
    {
        public static Produit ToDAL(IDataRecord record)
        {
            return new Produit
            {
                Id = (int)record["Id"],
                Nom = (string)record["Nom"],
                Description = (string)record["Description"],
                Categorie = (string)record["Categorie"],
                Quantite = (int)record["Quantite"],
                PrixHTVA = (decimal)record["PrixHTVA"],
                PrixTVAC = (decimal)record["PrixTVAC"]
            };
        }

        public static Dictionary<string, object> ToDB(Produit produit)
        {
            Dictionary<string, object> produitDictionnaire = new Dictionary<string, object>();
            produitDictionnaire.Add("@Nom", produit.Nom);
            produitDictionnaire.Add("@Description", produit.Description);
            produitDictionnaire.Add("@Categorie", produit.Categorie);
            produitDictionnaire.Add("@Quantite", produit.Quantite);
            produitDictionnaire.Add("@PrixHTVA", produit.PrixHTVA);
            produitDictionnaire.Add("@PrixTVAC", produit.PrixTVAC);

            return produitDictionnaire;

        }
    }
}
