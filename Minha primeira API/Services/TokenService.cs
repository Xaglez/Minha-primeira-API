using Minha_primeira_API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Minha_primeira_API.Services
{
    public class TokenService : ITokenService
    {
        public string GenerateToken(Users user)
        {

            if (user == null)
            {
                throw new Exception("Usuário vázio");
            }

            if (string.IsNullOrEmpty(user.Name))
            {
                throw new Exception("Nome vázio");
            }

            var secretKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY");
            
            if (secretKey == null )
            {
                throw new Exception("Insira a secret key");
            }

            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescripitor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Name.ToString()),
                    new Claim("IsAdmin", user.IsAdmin.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescripitor);

            return  tokenHandler.WriteToken(token);
        }
    }
}
