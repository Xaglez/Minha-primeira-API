using Microsoft.EntityFrameworkCore;
using Minha_primeira_API.Data;
using Minha_primeira_API.Models;

namespace Minha_primeira_API.Repositories
{
    public class UsersRepository : IUsersRepository
    {

        private readonly AppDbContext _context;

        public UsersRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<Users?> GetByNameAsync(string name)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Name == name);
        }

        public async Task BecomeAdmin(Users user)
        {
            user.IsAdmin = true;
            await _context.SaveChangesAsync();
        }

        public async Task CreateUserAsync(Users users)
        {
            _context.Users.Add(users);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(Users users)
        {
            _context.Set<Users>().Remove(users);
            await _context.SaveChangesAsync();
        }

        public async Task<Users> GetByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                throw new Exception("Não encontrado");
            }

            return (user);

        }

        public async Task UpdateByIdAsync(Users user, Users newUsers)
        {
            user.Name = newUsers.Name;
            user.Password = newUsers.Password;
            await _context.SaveChangesAsync();
        }

        public async Task<List<Users>> GetAllAsync()
        {
            var users = await _context.Users.ToListAsync();

            if (users == null || !users.Any())
            {
                throw new Exception("Não encontrado");
            }

            return users;
        }
    }
}
