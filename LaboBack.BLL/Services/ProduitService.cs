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

namespace LaboBack.BLL.Services
{
    public class ProduitService : IProduitService
    {
        private readonly IProduitRepository _repository;

        public ProduitService(IProduitRepository repository)
        {
            _repository = repository;
        }
        public int Create(Produit produit)
        {
            int id = _repository.Create(produit.BllToDal());
            return id;
        }

        public void Update(Produit produit)
        {
            var existing = _repository.GetById(produit.Id);
            if (existing == null)
            {
                throw new ArgumentException("Produit introuvable.");
            }
            _repository.Update(produit.BllToDal());
        }

        public IEnumerable<Produit> GetAll()
        {
            return _repository
                            .GetAll()
                            .Select(p => p.DalToBll());
        }

        public Produit? GetById(int id)
        {
            var produitDal = _repository.GetById(id);
            return produitDal?.DalToBll();
        }



        public IEnumerable<Produit> GetByCategorie(string categorie)
        {
            throw new NotImplementedException();
        }


        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
