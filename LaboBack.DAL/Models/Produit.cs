using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboBack.DAL.Models
{
    public class Produit
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }
        public string Categorie { get; set; }
        public int Quantite { get; set; }
        public decimal PrixHTVA { get; set; }
        public decimal PrixTVAC { get; set; }
    }
}
