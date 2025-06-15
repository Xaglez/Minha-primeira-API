namespace Minha_primeira_API.DTOs
{
    public class CreateSaleDTO
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public decimal TotalValue { get; set; }
        public DateTime DataVenda { get; set; }

    }
}
