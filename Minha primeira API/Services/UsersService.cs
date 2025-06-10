using Minha_primeira_API.Models;
using Minha_primeira_API.Repositories;
using Microsoft.Extensions.Logging;

namespace Minha_primeira_API.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly ITokenService _tokenService;
        private readonly ILogger<UsersService> _logger;

        public UsersService(IUsersRepository usersRepository, ITokenService tokenService, ILogger<UsersService> logger)
        {
            _usersRepository = usersRepository;
            _tokenService = tokenService;
            _logger = logger;
        }

        public async Task<Users?> GetByNameAsync(string name)
        {
            _logger.LogInformation("Buscando usuário por nome: {Name}", name);

            if (string.IsNullOrEmpty(name))
            {
                _logger.LogWarning("Tentativa de busca por nome vazio ou nulo.");
                return null;
            }

            return await _usersRepository.GetByNameAsync(name);
        }

        public async Task<string> AuthenticateAsync(int id)
        {
            _logger.LogInformation("Tentando autenticar usuário com ID {Id}.", id);

            var user = await _usersRepository.GetByIdAsync(id);

            if (user == null || user.Id != id)
            {
                _logger.LogError("Usuário com ID {Id} não encontrado.", id);
                throw new UnauthorizedAccessException("Id inválido");
            }
                
            var token = _tokenService.GenerateToken(user);

            return token;
        }

        public async Task BecomeAdmin(int id)
        {
            _logger.LogInformation("Tentando tornar o usuário com ID {Id} um administrador.", id);
            var user = await _usersRepository.GetByIdAsync(id);


            if (user == null)
            {
                _logger.LogError("Usuário com ID {Id} não encontrado.", id);
                throw new Exception("Não encontrado");
            }

            await _usersRepository.BecomeAdmin(user);
        }

        public async Task CreateUserAsync(Users users)
        {
            _logger.LogInformation("Criando novo usuário: {User}", users);
            if (users == null)
            {
                _logger.LogWarning("Tentativa de criação de usuário com dados nulos.");
                throw new Exception("Não encontrado");
            }

            await _usersRepository.CreateUserAsync(users);
        }

        public async Task DeleteByIdAsync(int id)
        {
            _logger.LogInformation("Tentando deletar usuário com ID {Id}.", id);
            var users = await _usersRepository.GetByIdAsync(id);

            if (users == null)
            {
                _logger.LogError("Usuário com ID {Id} não encontrado.", id);
                throw new Exception("Não encontrado");
            }

            await _usersRepository.DeleteByIdAsync(users);
        }

        public Task<Users> GetByIdAsync(int id)
        {
            _logger.LogInformation("Buscando usuário por ID: {Id}", id);
            var user = _usersRepository.GetByIdAsync(id);

            if (user == null)
            {
                _logger.LogError("Usuário com ID {Id} não encontrado.", id);
                throw new Exception("Não encontrado");
            }

            return user;
        }

        public async Task UpdateAsync(Users newUser)
        {
            _logger.LogInformation("Atualizando usuário: {User}", newUser);

            if (newUser == null)
            {
                _logger.LogWarning("Tentativa de atualização de usuário com dados nulos.");
                throw new Exception("Não encontrado");
            }

            await _usersRepository.UpdateAsync(newUser);
        }

        public async Task<List<Users>> GetAllAsync()
        {
            _logger.LogInformation("Buscando todos os usuários.");
            var users = await _usersRepository.GetAllAsync();
            return users;
        }
    }
}
