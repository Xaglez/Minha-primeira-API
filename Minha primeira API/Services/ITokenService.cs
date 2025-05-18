using Minha_primeira_API.Models;

namespace Minha_primeira_API.Services
{
    public interface ITokenService
    {
        string GenerateToken(Users user);
    }
}
