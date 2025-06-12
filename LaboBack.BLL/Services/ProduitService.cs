using LaboBack.BLL.Interfaces;
using LaboBack.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaboBack.BLL.Mappers;
using LaboBack.BLL.Models;
using System.Reflection.Metadata;
using LaboBack.DAL.Models;
using Produit = LaboBack.BLL.Models.Produit;
using LaboBack.DAL.Repositories;

namespace LaboBack.BLL.Services
{
    public class ProduitService : IProduitService
    {
        private readonly IProduitRepository _repository;

        public ProduitService(IProduitRepository repository)
        {
            _repository = repository;
        }

        //Création d'un produit
        public int Create(Produit produit)
        {
            int id = _repository.Create(produit.BllToDal());
            return id;
        }

        // Mise à jour d'un produit
        public void Update(Produit produit)
        {
            var existing = _repository.GetById(produit.Id);
            if (existing == null)
            {
                throw new ArgumentException("Produit introuvable.");
            }
            _repository.Update(produit.BllToDal());
        }

        //Récupération de l'ensemble des produits
        public IEnumerable<Produit> GetAll()
        {
            return _repository
                            .GetAll()
                            .Select(p => p.DalToBll());
        }

        //Récupération d'un produit
        public Produit? GetById(int id)
        {
            var produitDal = _repository.GetById(id);
            return produitDal?.DalToBll();
        }


        //Tri produit par catégorie
        public IEnumerable<Produit> GetByCategorie(string categorie)
        {
            return _repository.GetByCategorie(categorie).Select(p => p.DalToBll());
        }

        //Suppression produit
        public void Delete(int id)
        {
            var existing = _repository.GetById(id);
            if (existing == null)
            {
                throw new ArgumentException("Produit introuvable.");
            }
            _repository.Delete(id);
        }

        public IEnumerable<string> GetCategories()
        {
            return _repository.GetCategories();
        }
    }
}
