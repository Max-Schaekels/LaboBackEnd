using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboBack.DAL.Models
{
    public class Commande
    {
        public int Id { get; set; }
        public int UtilisateurId { get; set; }
        public DateTime DateCommande { get; set; }
        public string StatutCommande { get; set; }
    }
}
