using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaboBack.DAL.Models;

namespace LaboBack.DAL.Interfaces
{
    public interface IProduitRepository
    {
        IEnumerable<Produit> GetAll();
        Produit? GetById(int id);
        int Create(Produit produit);
        void Update(Produit produit);
        void Delete(int id);
        IEnumerable<Produit> GetByCategorie(string categorie);
    }
}
