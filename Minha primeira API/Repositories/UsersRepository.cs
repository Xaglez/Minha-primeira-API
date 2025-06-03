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

        public async Task UpdateAsync(Users user)
        {
            if (user == null)
            {
                throw new Exception("Não encontrado");
            }

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<Users> GetByEmailAndPasswordAsync(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Email e senha não podem ser vazios.");
            }

            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }

        public async Task<Users> GetByEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("Email não pode ser vazio.");
            }
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
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
