using LaboBack.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LaboBack.API.Mappers;
using LaboBack.API.Models;
using LaboBack.API.Models.DTO;
using LaboBack.API.Models.DTO.Produit;
using LaboBack.BLL.Services;


namespace LaboBack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProduitController : ControllerBase
    {
        private readonly IProduitService _produitService;

        public ProduitController(IProduitService produitService)
        {
            _produitService = produitService;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _produitService.GetAll().Select(p => p.BllToApi());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetByID(int id)
        {
            var result = _produitService.GetById(id);

            if (result is null)
            {
                return NotFound();
            }

            return Ok(result.BllToApi());
        }

        
        [HttpGet("categorie/{categorie}")]
        public IActionResult GetByCategorie(string categorie)
        {
            var result = _produitService.GetByCategorie(categorie).Select(p => p.BllToApi());
            return Ok(result);
        }

        [HttpPost(nameof(Create))]
        public IActionResult Create(ProduitFormDTO form)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                int id = _produitService.Create(form.ApiToBll());

                return Ok("Produit créer avec succès !!!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
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
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
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
