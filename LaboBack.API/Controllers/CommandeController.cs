using LaboBack.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LaboBack.API.Mappers;
using LaboBack.API.Models;
using LaboBack.API.Models.DTO;
using LaboBack.API.Models.DTO.Commande;
using LaboBack.BLL.Services;

namespace LaboBack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandeController : ControllerBase
    {
        private readonly ICommandeService _commandeService;

        public CommandeController(ICommandeService commandeService)
        {
            _commandeService = commandeService;
        }

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

        [HttpGet("utilisateur/{utilisateurId}")]
        public IActionResult GetByUtilisateurId(int utilisateurId)
        {
            var result = _commandeService.GetByUtilisateurId(utilisateurId);

            if (result is null)
            {
                return NotFound();
            }

            return Ok(result.Select(c => c.BllToApi()));
        }

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
                var lignes = form.Lignes.ToBll().ToList();           

                int id = _commandeService.Create(commande, lignes);  

                return CreatedAtAction(nameof(GetByID), new { id = id }, "Commande créée !");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{commandeId}")]
        public IActionResult UpdateStatut(int commandeId, [FromBody] StatutCommandeFormDTO nouveauStatut)
        {
            _commandeService.UpdateStatut(commandeId, nouveauStatut.StatutCommande);
            return Ok();
        }

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
    }
}
