using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Minha_primeira_API.Models
{
    public class Products
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo Obrigatorio")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "Campo Obrigatorio")]
        [StringLength(100, ErrorMessage = "O campo deve ter no máximo 100 caracteres")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Campo Obrigatorio")]
        [StringLength(50, ErrorMessage = "O campo deve ter no máximo 50 caracteres")]
        public string? Category { get; set; }

        [Required(ErrorMessage = "Campo Obrigatorio")]
        [Column(TypeName = "DECIMAL(10,2)")]
        public decimal Price { get; set; }
    }
}
