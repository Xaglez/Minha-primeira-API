using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Minha_primeira_API.DTOs;
using Minha_primeira_API.Models;
using Minha_primeira_API.Services;

namespace Minha_primeira_API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
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

        [Authorize(Policy = "AdminPolicy")]
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

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var product = await _usersService.GetByIdAsync(id);

            return Ok(product);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateByIdAsync(int id, [FromBody] UpdateUsersRequest newUser)
        {
            var user = await _usersService.GetByIdAsync(id);

            if (user == null)
                return NotFound("Usuário não encontrado");

            user.Password = newUser.Password;
            user.Email = newUser.Email;

            await _usersService.UpdateAsync(user);

            return NoContent();
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteByIdAsync(int id)
        {
            await _usersService.DeleteByIdAsync(id);

            return NoContent();
        }
    }
}
