using LaboBack.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LaboBack.API.Mappers;
using LaboBack.API.Models;
using LaboBack.API.Models.DTO;
using LaboBack.API.Models.DTO.Utilisateur;

namespace LaboBack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUtilisateurService _utilisateurService;

        public AuthController(IUtilisateurService utilisateurService)
        {
            _utilisateurService = utilisateurService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _utilisateurService.GetAll().Select(u => u.BllToApi());
            return Ok(result);
        }

        [HttpPost(nameof(Register))]
        public IActionResult Register(RegisterFormDTO form)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                int id = _utilisateurService.Register(form.ApiToBll());

                return Ok("Utilisateur enregitré avec succès !!!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost(nameof(Login))]
        public IActionResult Login(LoginFormDTO form)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var userBll = _utilisateurService.Login(form.Email, form.Mdp);

                if (userBll == null)
                {
                    return BadRequest("Email ou mot de passe incorrect.");
                }

                var userApi = userBll.BllToApi();
                return Ok(userApi);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{email}")]
        public IActionResult Update(string email, UpdateFormDTO form)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var utilisateur = form.ApiToBll();
                utilisateur.Email = email;
                _utilisateurService.Update(utilisateur);


                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
