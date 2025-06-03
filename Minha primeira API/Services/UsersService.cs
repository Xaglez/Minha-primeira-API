using Minha_primeira_API.Models;
using Minha_primeira_API.Repositories;

namespace Minha_primeira_API.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly ITokenService _tokenService;

        public UsersService(IUsersRepository usersRepository, ITokenService tokenService)
        {
            _usersRepository = usersRepository;
            _tokenService = tokenService;
        }

        public async Task<Users?> GetByNameAsync(string name)
        {
            return await _usersRepository.GetByNameAsync(name);
        }

        public async Task<string> AuthenticateAsync(int id)
        {
            var user = await _usersRepository.GetByIdAsync(id);

            if (user == null || user.Id != id)
                throw new UnauthorizedAccessException("Id inválido");

            var token = _tokenService.GenerateToken(user);

            return token;
        }

        public async Task BecomeAdmin(int id)
        {
            var user = await _usersRepository.GetByIdAsync(id);

            if (user == null)
            {
                throw new Exception("Não encontrado");
            }

            await _usersRepository.BecomeAdmin(user);
        }

        public async Task CreateUserAsync(Users users)
        {
            if (users == null)
            {
                throw new Exception("Não encontrado");
            }

            await _usersRepository.CreateUserAsync(users);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var users = await _usersRepository.GetByIdAsync(id);

            if (users == null)
            {
                throw new Exception("Não encontrado");
            }

            await _usersRepository.DeleteByIdAsync(users);
        }

        public Task<Users> GetByIdAsync(int id)
        {
            var user = _usersRepository.GetByIdAsync(id);

            if (user == null)
            {
                throw new Exception("Não encontrado");
            }

            return user;
        }

        public async Task UpdateAsync(Users newUser)
        {

            if (newUser == null)
            {
                throw new Exception("Não encontrado");
            }

            await _usersRepository.UpdateAsync(newUser);
        }

        public async Task<List<Users>> GetAllAsync()
        {
            var users = await _usersRepository.GetAllAsync();
            return users;
        }
    }
}
