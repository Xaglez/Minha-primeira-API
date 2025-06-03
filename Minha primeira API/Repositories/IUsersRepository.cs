using Minha_primeira_API.Models;

namespace Minha_primeira_API.Repositories
{
    public interface IUsersRepository
    {
        Task CreateUserAsync(Users users);
        Task<Users> GetByIdAsync(int id);
        Task UpdateAsync(Users user);
        Task DeleteByIdAsync(Users users);
        Task BecomeAdmin(Users user);
        Task<Users?> GetByNameAsync(string name);
        Task<List<Users>> GetAllAsync();
        Task<Users> GetByEmailAndPasswordAsync(string email, string password);
        Task<Users> GetByEmailAsync(string email);
    }
}
