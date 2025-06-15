using Minha_primeira_API.DTOs;
using Minha_primeira_API.Models;
using Minha_primeira_API.Repositories;

namespace Minha_primeira_API.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;

        public SaleService(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task CreateSaleAsync(Venda sale)
        {
            if (sale == null)
            {
                throw new ArgumentNullException(nameof(sale), "A venda não pode ser nula.");
            }

            await _saleRepository.CreateSaleAsync(sale);
        }

        public Task DeleteByIdAsync(int id)
        {
            var sale = _saleRepository.GetByIdAsync(id);

            if (sale == null)
            {
                throw new ArgumentNullException(nameof(sale), "A venda não pode ser nula.");
            }

            return _saleRepository.DeleteByIdAsync(id);
        }

        public async Task<List<Venda>> GetAllAsync()
        {
            var sales = await _saleRepository.GetAllAsync();

            if (sales == null || !sales.Any())
            {
                throw new Exception("Não foi possível encontrar vendas.");
            }

            return sales;
        }

        public async Task<List<Venda>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var sales = await _saleRepository.GetByDateRangeAsync(startDate, endDate);

            if (sales == null || !sales.Any())
            {
                throw new Exception("Nenhuma venda encontrada no intervalo de datas especificado.");
            }

            return sales;
        }

        public async Task<Venda> GetByIdAsync(int id)
        {
            var sale = await _saleRepository.GetByIdAsync(id);

            if (sale == null)
            {
                throw new Exception("Venda não encontrada.");
            }

            return sale;
        }

        public async Task<List<Venda>> GetByProductIdAsync(int productId)
        {
            var sales = await _saleRepository.GetByProductIdAsync(productId);

            if (sales == null || !sales.Any())
            {
                throw new Exception("Nenhuma venda encontrada para o produto especificado.");
            }

            return sales;
        }

        public Task<List<Venda>> GetByUserIdAsync(int userId)
        {
            var sales = _saleRepository.GetByUserIdAsync(userId);

            if (sales == null || !sales.Result.Any())
            {
                throw new Exception("Nenhuma venda encontrada para o usuário especificado.");
            }

            return sales;
        }

        public async Task UpdateByIdAsync(int id, Venda newSale)
        {
            var existingSale = await _saleRepository.GetByIdAsync(id);

            if (existingSale == null)
            {
                throw new Exception("Venda não encontrada para atualização.");
            }

            if (newSale == null)
            {
                throw new ArgumentNullException(nameof(newSale), "A nova venda não pode ser nula.");
            }

            await _saleRepository.UpdateByIdAsync(id, newSale);
        }
    }
}
