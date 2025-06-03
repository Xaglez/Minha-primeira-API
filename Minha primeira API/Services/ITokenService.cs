using Minha_primeira_API.Models;
using System.Security.Claims;

namespace Minha_primeira_API.Services
{
    public interface ITokenService
    {
        string GenerateToken(Users user);
        string GenereteRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
