using LaboBack.BLL.Interfaces;
using LaboBack.API.Models.DTO.Utilisateur;
using LaboBack.API.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LaboBack.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUtilisateurService _utilisateurService;

        public UserController(IUtilisateurService utilisateurService)
        {
            _utilisateurService = utilisateurService;
        }

        // GET /api/User/{id}: Récupère les infos du profil connecté 
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            string? userIdClaim = User.FindFirst(ClaimTypes.Sid)?.Value;
            if (userIdClaim == null || int.Parse(userIdClaim) != id)
                return Forbid();

            var userBll = _utilisateurService.GetById(id);
            if (userBll == null)
                return NotFound();

            return Ok(userBll.BllToApi());
        }

        // PUT /api/User/{id} : Modifie son propre profil (Nom, Prénom, Mdp)
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateFormDTO form)
        {
            string? userIdClaim = User.FindFirst(ClaimTypes.Sid)?.Value;
            string? roleClaim = User.FindFirst(ClaimTypes.Role)?.Value?.ToLowerInvariant();

            if (userIdClaim == null || int.Parse(userIdClaim) != id)
                return Forbid("Vous ne pouvez modifier que votre propre profil.");

            
            var existingUser = _utilisateurService.GetById(id);
            if (existingUser == null)
                return NotFound("Utilisateur non trouvé.");

            // Si MDP non fourni, on garde l'ancien
            if (string.IsNullOrWhiteSpace(form.Mdp))
            {
                form.Mdp = existingUser.Mdp;
            }

            var userToUpdate = form.ApiToBll();
            userToUpdate.Id = id;

            // Si non-admin, on bloque la modif de rôle
            if (roleClaim != "admin")
            {
                userToUpdate.Role = null;
            }

            bool updated = _utilisateurService.Update(userToUpdate);
            if (!updated)
                return BadRequest("La mise à jour a échoué.");

            return NoContent();
        }
    }
}