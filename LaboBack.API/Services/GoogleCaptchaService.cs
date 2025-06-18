using LaboBack.API.Models;
using System.Text.Json;

namespace LaboBack.API.Services
{
    public class GoogleCaptchaService
    {
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;

        public GoogleCaptchaService(IConfiguration config, HttpClient httpClient)
        {
            _config = config;
            _httpClient = httpClient;
        }

        public async Task<bool> VerifyTokenAsync(string token)
        {
            string secret = _config["GoogleCaptcha:SecretKey"];
            var response = await _httpClient.PostAsync(
            $"https://www.google.com/recaptcha/api/siteverify?secret={secret}&response={token}",
             null
            );

            if (response is not null)
            {
                var json = await response.Content.ReadAsStringAsync();
                GoogleCaptchaResponse result = JsonSerializer.Deserialize<GoogleCaptchaResponse>(json);
                return result.success;
            }

            return false;

        }
    }
}
