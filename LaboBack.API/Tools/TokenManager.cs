using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LaboBack.API.Tools
{
    public class TokenManager
    {
        private readonly IConfiguration _configuration;
        private readonly string _secret;
        private readonly string _issuer;
        private readonly string _audience;

        public TokenManager(IConfiguration configuration)
        {
            _configuration = configuration;
            _secret = _configuration["jwt:key"];
            _issuer = _configuration["jwt:issuer"];
            _audience = _configuration["jwt:audience"];
        }

        public string GenerateJwt(dynamic user, int expirationDate = 1)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

            DateTime now = DateTime.Now;

            Claim[] myclaims = new Claim[]
            {
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.GivenName, user.Nom ?? "NomInconnu"),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.Expiration, now.AddHours(expirationDate).ToString(), ClaimValueTypes.DateTime)
            };

            JwtSecurityToken token = new JwtSecurityToken(
                claims: myclaims,
                expires: now.AddHours(expirationDate),
                signingCredentials: credentials,
                audience: _audience,
                issuer: _issuer
            );

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);
        }
    }
}
