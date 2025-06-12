using LaboBack.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LaboBack.API.Mappers;
using LaboBack.API.Models;
using LaboBack.API.Models.DTO;
using LaboBack.API.Models.DTO.Produit;
using LaboBack.BLL.Services;
using Microsoft.AspNetCore.Authorization;


namespace LaboBack.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProduitController : ControllerBase
    {
        private readonly IProduitService _produitService;

        public ProduitController(IProduitService produitService)
        {
            _produitService = produitService;
        }

        // Récupération de l'ensemble des produits
        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAll()
        {
            var result = _produitService.GetAll().Select(p => p.BllToApi());
            return Ok(result);
        }

        // Récupération d'un produit par son id (détails)
        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult GetByID(int id)
        {
            var result = _produitService.GetById(id);

            if (result is null)
            {
                return NotFound();
            }

            return Ok(result.BllToApi());
        }

        // Récupération des catégories
        [HttpGet("categories")]
        [AllowAnonymous] 
        public IActionResult GetCategories()
        {
            try
            {
                var categories = _produitService.GetCategories();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Tri des produits par catégories
        [HttpGet("categorie/{categorie}")]
        [AllowAnonymous]
        public IActionResult GetByCategorie(string categorie)
        {
            var result = _produitService.GetByCategorie(categorie).Select(p => p.BllToApi());
            return Ok(result);
        }

        //Création d'un produit
        [HttpPost(nameof(Create))]
        [Authorize(Roles = "admin")]
        public IActionResult Create(ProduitFormDTO form)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                int id = _produitService.Create(form.ApiToBll());

                var produitCree = _produitService.GetById(id); 

                return Ok(produitCree?.BllToApi()); // envoie du Product (avec Id) au front
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Update d'un produit sur base de son id
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult Update(int id, UpdateProduitFormDTO form)
        {
            try
            {
                if (!ModelState.IsValid)
                {

                    return BadRequest(ModelState);
                }

                var produit = form.ApiToBll();
                produit.Id = id;

                _produitService.Update(produit);
                var updated = _produitService.GetById(id); // <- on récupère le produit mis à jour
                return Ok(updated?.BllToApi()); // <- on le renvoie
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Suppression d'un produit
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult Delete(int id)
        {
            try
            {

                _produitService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
