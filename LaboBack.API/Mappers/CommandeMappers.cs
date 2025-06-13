using bll = LaboBack.BLL.Models;
using api = LaboBack.API.Models;
using LaboBack.API.Models.DTO.Commande;

namespace LaboBack.API.Mappers
{
    public static class CommandeMappers
    {
        public static bll.Commande ApiToBll(this CommandeFormDTO form)
        {
            return new bll.Commande
            {
                
                DateCommande = DateTime.Now, 
                StatutCommande = "En cours"  
            };
        }

        public static api.Commande BllToApi(this bll.Commande commande)
        {
            return new api.Commande
            {
                Id = commande.Id,
                UtilisateurId = commande.UtilisateurId,
                DateCommande = commande.DateCommande,
                StatutCommande = commande.StatutCommande
            };
        }
        public static CommandeDTO BllToDto(this bll.Commande commande)
        {
            return new CommandeDTO
            {
                Id = commande.Id,
                UtilisateurId = commande.UtilisateurId,
                Date = commande.DateCommande,
                Statut = commande.StatutCommande
            };
        }

        public static bll.Commande_Produit ToBll(this LigneCommandeFormDTO ligne)
        {
            return new bll.Commande_Produit
            {
                ProduitId = ligne.ProduitId,
                QuantiteCommandee = ligne.QuantiteCommandee
            };
        }

        public static IEnumerable<bll.Commande_Produit> ToBll(this IEnumerable<LigneCommandeFormDTO> lignes)
        {
            return lignes.Select(l => l.ToBll());
        }

        public static string ToStatut(this StatutCommandeFormDTO dto)
        {
            return dto.StatutCommande;
        }

        public static api.Commande_Produit BllToApi(this bll.Commande_Produit ligne)
        {
            return new api.Commande_Produit
            {
                CommandeId = ligne.CommandeId,
                ProduitId = ligne.ProduitId,
                QuantiteCommandee = ligne.QuantiteCommandee
            };
        }

        public static api.CommandeDetail BllToApi(this bll.CommandeDetail detail)
        {
            return new api.CommandeDetail
            {
                CommandeId = detail.CommandeId,
                ProduitId = detail.ProduitId,
                Nom = detail.Nom,
                Categorie = detail.Categorie,
                PrixHTVA = detail.PrixHTVA,
                PrixTVAC = detail.PrixTVAC,
                QuantiteCommandee = detail.QuantiteCommandee
            };
        }
    }
}
