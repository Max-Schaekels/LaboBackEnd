using LaboBack.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LaboBack.API.Mappers;
using LaboBack.API.Models;
using LaboBack.API.Models.DTO;
using LaboBack.API.Models.DTO.Commande;
using LaboBack.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace LaboBack.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CommandeController : ControllerBase
    {
        private readonly ICommandeService _commandeService;


        public CommandeController(ICommandeService commandeService)
        {
            _commandeService = commandeService;
        }

        // Récupération de la commande par l'id
        [HttpGet("{id}")]
        public IActionResult GetByID(int id)
        {
            var result = _commandeService.GetById(id);

            if (result is null)
            {
                return NotFound();
            }

            return Ok(result.BllToApi());
        }

        // Récupération de la commande avec l'id de l'utilisateur
        [HttpGet("utilisateur/{utilisateurId}")]
        public IActionResult GetByUtilisateurId(int utilisateurId)
        {
            var result = _commandeService.GetByUtilisateurId(utilisateurId);

            if (result is null)
            {
                return NotFound();
            }

            return Ok(result.Select(c => c.BllToDto()));
        }

        // Récupération des lignes de la commande (quantité comandée)
        [HttpGet("commande/{commandeId}")]
        public IActionResult GetLignesCommande(int commandeId)
        {
            var result = _commandeService.GetLignesCommande(commandeId);

            if (result is null)
            {
                return NotFound();
            }

            return Ok(result.Select(c => c.BllToApi()));
        }

        //Création d'une commande
        [HttpPost(nameof(Create))]
        public IActionResult Create([FromBody] CommandeFormDTO form)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var commande = form.ApiToBll();
                commande.UtilisateurId = int.Parse(User.FindFirst(ClaimTypes.Sid).Value);
                var lignes = form.Lignes.ToBll().ToList();           

                int id = _commandeService.Create(commande, lignes);

                var commandeCreee = _commandeService.GetById(id);
                return CreatedAtAction(nameof(GetByID), new { id = id }, commandeCreee.BllToApi());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Modification du status de la commande sur base de son id
        [HttpPatch("{commandeId}")]
        [Authorize(Roles = "admin")]
        public IActionResult UpdateStatut(int commandeId, [FromBody] StatutCommandeFormDTO nouveauStatut)
        {
            _commandeService.UpdateStatut(commandeId, nouveauStatut.StatutCommande);
            return Ok();
        }

        //Détails de la commande (nom du produit, catégorie, quantité commandé et les prix)
        [HttpGet("{commandeId}/details")]
        public IActionResult GetCommandeDetails(int commandeId)
        {
            var result = _commandeService.GetCommandeDetails(commandeId);

            if (!result.Any())
            {
                return NotFound();
            }

            return Ok(result.Select(c => c.BllToApi()));
        }

        //récupération des commandes par l'admin 
        [HttpGet]
        [Authorize(Roles = "admin")] 
        public IActionResult GetAll()
        {
            var commandes = _commandeService.GetAll();
            return Ok(commandes.Select(c => c.BllToApi()));
        }
    }
}
