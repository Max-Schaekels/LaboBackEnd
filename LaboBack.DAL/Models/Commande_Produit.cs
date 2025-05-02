using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboBack.DAL.Models
{
    public class Commande_Produit
    {
        public int CommandeId { get; set; }
        public int ProduitId { get; set; }
        public int QuantiteCommandee { get; set; }
    }
}
