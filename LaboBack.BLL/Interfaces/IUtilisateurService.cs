using LaboBack.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboBack.BLL.Interfaces
{
    public interface IUtilisateurService
    {
        int Register(Utilisateur utilisateur);
        Utilisateur? Login(string email, string mdp);
        IEnumerable<Utilisateur> GetAll();
        Utilisateur? GetById(int id);
        Utilisateur? GetByEmail(string email);
        public void Update(Utilisateur utilisateur);
      
    }
}
