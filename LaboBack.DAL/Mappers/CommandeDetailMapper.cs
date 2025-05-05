using LaboBack.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboBack.DAL.Mappers
{
    public class CommandeDetailMapper
    {
        public static CommandeDetail ToDAL(IDataRecord record)
        {
            return new CommandeDetail
            {
                CommandeId = (int)record["CommandeId"],
                ProduitId = (int)record["ProduitId"],
                Nom = (string)record["Nom"],
                Categorie = (string)record["Categorie"],
                PrixHTVA = (decimal)record["PrixHTVA"],
                PrixTVAC = (decimal)record["PrixTVAC"],
                QuantiteCommandee = (int)record["QuantiteCommandee"]


            };
        }


    }
}
