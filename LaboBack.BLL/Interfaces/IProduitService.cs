using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaboBack.BLL.Models;

namespace LaboBack.BLL.Interfaces
{
    public interface IProduitService
    {
        IEnumerable<Produit> GetAll();
        Produit? GetById(int id);
        int Create(Produit produit);
        void Update(Produit produit);
        void Delete(int id);
        IEnumerable<Produit> GetByCategorie(string categorie);
        IEnumerable<string> GetCategories();
    }
}
