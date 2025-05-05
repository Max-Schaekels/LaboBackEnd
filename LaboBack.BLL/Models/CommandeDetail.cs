using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboBack.BLL.Models
{
    public class CommandeDetail
    {
        public int CommandeId { get; set; }
        public int ProduitId { get; set; }
        public string Nom { get; set; }
        public string Categorie { get; set; }
        public decimal PrixHTVA { get; set; }
        public decimal PrixTVAC { get; set; }
        public int QuantiteCommandee { get; set; }
    }
}
