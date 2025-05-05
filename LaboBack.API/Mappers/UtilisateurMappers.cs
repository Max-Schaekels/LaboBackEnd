using bll = LaboBack.BLL.Models;
using api = LaboBack.API.Models;
using LaboBack.API.Models.DTO.Utilisateur;


namespace LaboBack.API.Mappers
{
    public static class UtilisateurMappers
    {
        public static bll.Utilisateur ApiToBll(this RegisterFormDTO form)
        {
            return new bll.Utilisateur
            {
                Nom = form.Nom,
                Prenom = form.Prenom,
                Email = form.Email,
                Mdp = form.Mdp,
                Role = form.Role?.ToLowerInvariant()
            };
        }

        public static api.Utilisateur BllToApi(this bll.Utilisateur form)
        {
            return new api.Utilisateur
            {
                Id = form.Id,
                Nom = form.Nom,
                Prenom = form.Prenom,
                Email = form.Email,
                Role = form.Role
            };
        }

        public static bll.Utilisateur ApiToBll(this UpdateFormDTO form)
        {
            return new bll.Utilisateur
            {
                Nom = form.Nom,
                Prenom = form.Prenom,
                Mdp = form.Mdp,
                Role = form.Role?.ToLowerInvariant()
            };
        }
    }
}
