using LaboBack.API.Models.DTO;
using LaboBack.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LaboBack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaptchaController : ControllerBase
    {
        private readonly GoogleCaptchaService _captchaService;

        public CaptchaController(GoogleCaptchaService captchaService)
        {
            _captchaService = captchaService;
        }

        [HttpPost("verify", Name ="Verify captcha")]
        public async Task<IActionResult> VerifyCaptcha([FromBody] CaptchaDTO dto)
        {
            if (dto is null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            bool isValid = await _captchaService.VerifyTokenAsync(dto.Token);
            if (!isValid) {
                return BadRequest(new { message = "Invalid captach token." });
            }

            return NoContent();


        }
    }
}
