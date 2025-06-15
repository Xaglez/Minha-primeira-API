namespace Minha_primeira_API.Models
{
    public class Venda
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public Users Users { get; set; }

        public int ProductId { get; set; }
        public Products Product { get; set; }

        public int Amount { get; set; }

        public decimal TotalValue { get; set; }

        public DateTime DataVenda { get; set; }
    }
}
