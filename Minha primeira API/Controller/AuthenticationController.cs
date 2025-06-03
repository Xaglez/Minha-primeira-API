using Microsoft.AspNetCore.Mvc;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Minha_primeira_API.DTOs;
using Minha_primeira_API.Models;
using Minha_primeira_API.Services;
using Minha_primeira_API.Repositories;

namespace Minha_primeira_API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly TokenService _tokenService;
        private readonly IUsersRepository _usuarioRepository;

        public AuthenticationController(TokenService tokenService, IUsersRepository usuarioRepository)
        {
            _tokenService = tokenService;
            _usuarioRepository = usuarioRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var usuario = await _usuarioRepository.GetByEmailAndPasswordAsync(request.Email, request.Password);
            if (usuario == null)
                return Unauthorized("Credenciais inválidas.");

            var token = _tokenService.GenerateToken(usuario);
            var refreshToken = _tokenService.GenereteRefreshToken();

            usuario.RefreshToken = refreshToken;
            usuario.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _usuarioRepository.UpdateAsync(usuario);

            return Ok(new
            {
                Token = token,
                RefreshToken = refreshToken
            });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
        {
            var principal = _tokenService.GetPrincipalFromExpiredToken(request.Token);
            var email = principal.Identity.Name;

            var usuario = await _usuarioRepository.GetByEmailAsync(email);
            if (usuario == null || usuario.RefreshToken != request.RefreshToken || usuario.RefreshTokenExpiryTime <= DateTime.UtcNow)
                return Unauthorized("Refresh token inválido.");

            var novoToken = _tokenService.GenerateToken(usuario);
            var novoRefreshToken = _tokenService.GenereteRefreshToken();

            usuario.RefreshToken = novoRefreshToken;
            usuario.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _usuarioRepository.UpdateAsync(usuario);

            return Ok(new
            {
                Token = novoToken,
                RefreshToken = novoRefreshToken
            });
        }
    }
}
