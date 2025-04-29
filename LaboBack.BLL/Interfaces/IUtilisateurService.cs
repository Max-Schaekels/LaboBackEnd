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
    }
}
