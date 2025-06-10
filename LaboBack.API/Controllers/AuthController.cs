using LaboBack.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LaboBack.API.Mappers;
using LaboBack.API.Models;
using LaboBack.API.Models.DTO;
using LaboBack.API.Models.DTO.Utilisateur;
using LaboBack.API.Tools;

namespace LaboBack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUtilisateurService _utilisateurService;
        private readonly TokenManager _tokenManager;

        //Service et token manager
        public AuthController(IUtilisateurService utilisateurService, TokenManager tokenManager)
        {
            _utilisateurService = utilisateurService;
            _tokenManager = tokenManager;
        }

        // liste de tous les utilisateurs
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _utilisateurService.GetAll().Select(u => u.BllToApi());
            return Ok(result);
        }

        //Enregistrement
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

        //Connexion
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

                string token = _tokenManager.GenerateJwt(userApi);
                return new JsonResult(new { user = userApi, token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Update de l'utilisateur via l'email
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
