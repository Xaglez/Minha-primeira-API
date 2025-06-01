using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Minha_primeira_API.Models;
using Minha_primeira_API.Services;

namespace Minha_primeira_API.Controller
{
    [ApiController]
    [Route("Users")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly ITokenService _tokenService;

        public UsersController(IUsersService usersService, ITokenService tokenService)
        {
            _usersService = usersService;
            _tokenService = tokenService;
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
            return Ok(new { message = "Acesso autorizado!" });
        }

        [Authorize (Policy = "AdminPolicy")]
        [HttpGet("listusers")]
        public async Task<IActionResult> GetAllAsync()
        {
            var users = await _usersService.GetAllAsync();
            if (users == null || !users.Any())
            {
                return NotFound("Nenhum usuário encontrado.");
            }
            return Ok(users);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Users model)
        {
            if (string.IsNullOrEmpty(model.Name))
            {
                return BadRequest(new { message = "Nome vazio." });
            }

            var user = await _usersService.GetByNameAsync(model.Name);

            if (user == null)
            {
                return NotFound(new { message = "Usuário não encontrado." });
            }

            if (user.Password != model.Password)
            {
                return BadRequest(new { message = "Senha incorreta." });
            }

            var token = _tokenService.GenerateToken(user);

            return Ok(new
            {
                user = new { user.Id, user.Name, user.IsAdmin },
                token = token
            });
        }

        [HttpPost]
        [Route("createusers")]
        public async Task<IActionResult> CreateUserAsync([FromBody] Users user)
        {
            try
            {
                await _usersService.CreateUserAsync(user);
                return Ok(user);
            }
            catch
            {
                return BadRequest("Produto vazio");
            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var product = await _usersService.GetByIdAsync(id);

            return Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateByIdAsync(int id, [FromBody] Users newUser)
        {
            await _usersService.UpdateByIdAsync(id, newUser);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteByIdAsync(int id)
        {
            await _usersService.DeleteByIdAsync(id);

            return NoContent();
        }
    }
}
