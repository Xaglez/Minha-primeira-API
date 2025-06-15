using Minha_primeira_API.Models;

namespace Minha_primeira_API.Services
{
    public interface ISaleService
    {
        Task CreateSaleAsync(Venda sale);
        Task<Venda> GetByIdAsync(int id);
        Task UpdateByIdAsync(int id, Venda newSale);
        Task DeleteByIdAsync(int id);
        Task<List<Venda>> GetAllAsync();
        Task<List<Venda>> GetByUserIdAsync(int userId);
        Task<List<Venda>> GetByProductIdAsync(int productId);
        Task<List<Venda>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
    }
}
