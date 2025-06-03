using Minha_primeira_API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.Security.Cryptography;

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
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim("IsAdmin", user.IsAdmin.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescripitor);

            return  tokenHandler.WriteToken(token);
        }

        public string GenereteRefreshToken()
        {
            var numeroAleatorio = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(numeroAleatorio);
            return Convert.ToBase64String(numeroAleatorio);
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var secretKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY");

            if (string.IsNullOrEmpty(secretKey))
                throw new InvalidOperationException("JWT_SECRET_KEY não foi configurada.");

            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Token inválido");

            return principal;
        }
    }
}
