﻿using Minha_primeira_API.Migrations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Minha_primeira_API.Models
{
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Insira um nome")]
        [StringLength(150, ErrorMessage = "Caracteres máximo não pode ser maior que 150")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Campo obrigatorio")]
        public bool IsAdmin { get; set; }

        [Required(ErrorMessage = "Insira um nome")]
        [StringLength(200, ErrorMessage = "Caracteres máximo não pode ser maior que 200")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Campo obrigatorio")]
        [StringLength(15, ErrorMessage = "Senha tem que ser maior que 8 e até 15 caracteres", MinimumLength = 8)]
        public string? Password { get; set; }

        public string? RefreshToken { get; set; }
        
        public DateTime RefreshTokenExpiryTime { get; set; }

        public List<Venda> Vendas { get; set; }
    }
}
