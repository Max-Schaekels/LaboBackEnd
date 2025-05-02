using LaboBack.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboBack.DAL.Interfaces
{
    public interface IUtilisateurRepository
    {
        IEnumerable<Utilisateur> GetAll();
        Utilisateur? GetById(int id);
        Utilisateur? GetByEmail(string email);
        string? GetPassword(string email);
        int Create(Utilisateur utilisateur);
        void Update(Utilisateur utilisateur);
    }
}
