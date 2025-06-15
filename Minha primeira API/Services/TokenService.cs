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
        private readonly ILogger<TokenService> _logger;

        public TokenService(ILogger<TokenService> logger)
            => _logger = logger;

        public string GenerateToken(Users user)
        {
            _logger.LogInformation("Gerando token para o usuário: {UserId}", user?.UserId);

            if (user == null)
            {
                _logger.LogError("Usuário é nulo ao tentar gerar token.");
                throw new Exception("Usuário vázio");
            }

            if (string.IsNullOrEmpty(user.Name))
            {
                _logger.LogError("Nome do usuário é vazio ao tentar gerar token.");
                throw new Exception("Nome vázio");
            }

            var secretKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY");
            
            if (secretKey == null )
            {
                _logger.LogError("JWT_SECRET_KEY não foi configurada.");
                throw new Exception("Insira a secret key");
            }

            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescripitor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim("IsAdmin", user.IsAdmin.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescripitor);

            _logger.LogInformation("Token gerado com sucesso para o usuário: {UserId}", user.UserId);
            return  tokenHandler.WriteToken(token);
        }

        public string GenereteRefreshToken()
        {
            _logger.LogInformation("Gerando Refresh Token.");
            var numeroAleatorio = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(numeroAleatorio);
            _logger.LogInformation("Refresh Token gerado com sucesso.");
            return Convert.ToBase64String(numeroAleatorio);
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            _logger.LogInformation("Validando token expirado.");
            var secretKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY");

            if (string.IsNullOrEmpty(secretKey))
            {
                _logger.LogError("JWT_SECRET_KEY não foi configurada.");
                throw new InvalidOperationException("JWT_SECRET_KEY não foi configurada.");
            }
                
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

            _logger.LogInformation("Token expirado validado com sucesso.");
            return principal;
        }
    }
}
