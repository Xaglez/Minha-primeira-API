using Minha_primeira_API.Models;

namespace Minha_primeira_API.Repositories
{
    public interface IUsersRepository
    {
        Task CreateUserAsync(Users users);
        Task<Users> GetByIdAsync(int id);
        Task UpdateByIdAsync(Users user, Users newUsers);
        Task DeleteByIdAsync(Users users);
        Task BecomeAdmin(Users user);
        Task<Users?> GetByNameAsync(string name);
        Task<List<Users>> GetAllAsync();
    }
}
