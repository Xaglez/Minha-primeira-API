using Minha_primeira_API.Models;

namespace Minha_primeira_API.Services
{
    public interface IUsersService
    {
        Task CreateUserAsync(Users users);
        Task<Users> GetByIdAsync(int id);
        Task UpdateAsync(Users newUser);
        Task DeleteByIdAsync(int id);
        Task BecomeAdmin(int id);
        Task<string> AuthenticateAsync(int id);
        Task<Users?> GetByNameAsync(string name);
        Task<List<Users>> GetAllAsync();
    }
}
