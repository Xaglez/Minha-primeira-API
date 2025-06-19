using Minha_primeira_API.DTOs;
using Minha_primeira_API.Models;
using Minha_primeira_API.Repositories;

namespace Minha_primeira_API.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly ILogger<SaleService> _logger;

        public SaleService(ISaleRepository saleRepository, ILogger<SaleService> logger)
        {
            _saleRepository = saleRepository;
            _logger = logger;
        }

        public async Task CreateSaleAsync(Venda sale)
        {
            if (sale == null)
            {
                _logger.LogError("Tentativa de criar uma venda nula.");
                throw new ArgumentNullException(nameof(sale), "A venda não pode ser nula.");
            }

            _logger.LogInformation("Criando uma nova venda para o usuário {UserId} e produto {ProductId}.", sale.UserId, sale.ProductId);
            await _saleRepository.CreateSaleAsync(sale);
        }

        public Task DeleteByIdAsync(int id)
        {
            var sale = _saleRepository.GetByIdAsync(id);

            if (sale == null)
            {
                _logger.LogError("Tentativa de deletar uma venda com ID {SaleId} que não existe.", id);
                throw new ArgumentNullException(nameof(sale), "A venda não pode ser nula.");
            }

            _logger.LogInformation("Deletando a venda com ID {SaleId}.", id);
            return _saleRepository.DeleteByIdAsync(id);
        }

        public async Task<List<Venda>> GetAllAsync()
        {
            var sales = await _saleRepository.GetAllAsync();

            if (sales == null || !sales.Any())
            {
                _logger.LogWarning("Nenhuma venda encontrada.");
                throw new Exception("Não foi possível encontrar vendas.");
            }

            _logger.LogInformation("Total de vendas encontradas: {Count}", sales.Count);
            return sales;
        }

        public async Task<List<Venda>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var sales = await _saleRepository.GetByDateRangeAsync(startDate, endDate);

            if (sales == null || !sales.Any())
            {
                _logger.LogWarning("Nenhuma venda encontrada no intervalo de datas {StartDate} a {EndDate}.", startDate, endDate);
                throw new Exception("Nenhuma venda encontrada no intervalo de datas especificado.");
            }

            _logger.LogInformation("Total de vendas encontradas no intervalo de datas: {Count}", sales.Count);
            return sales;
        }

        public async Task<Venda> GetByIdAsync(int id)
        {
            var sale = await _saleRepository.GetByIdAsync(id);

            if (sale == null)
            {
                _logger.LogError("Venda com ID {SaleId} não encontrada.", id);
                throw new Exception("Venda não encontrada.");
            }

            _logger.LogInformation("Venda encontrada: {SaleId}", id);
            return sale;
        }

        public async Task<List<Venda>> GetByProductIdAsync(int productId)
        {
            var sales = await _saleRepository.GetByProductIdAsync(productId);

            if (sales == null || !sales.Any())
            {
                _logger.LogWarning("Nenhuma venda encontrada para o produto com ID {ProductId}.", productId);
                throw new Exception("Nenhuma venda encontrada para o produto especificado.");
            }

            _logger.LogInformation("Total de vendas encontradas para o produto {ProductId}: {Count}", productId, sales.Count);
            return sales;
        }

        public Task<List<Venda>> GetByUserIdAsync(int userId)
        {
            var sales = _saleRepository.GetByUserIdAsync(userId);

            if (sales == null || !sales.Result.Any())
            {
                _logger.LogWarning("Nenhuma venda encontrada para o usuário com ID {UserId}.", userId);
                throw new Exception("Nenhuma venda encontrada para o usuário especificado.");
            }

            _logger.LogInformation("Total de vendas encontradas para o usuário {UserId}: {Count}", userId, sales.Result.Count);
            return sales;
        }

        public async Task UpdateByIdAsync(int id, Venda newSale)
        {
            var existingSale = await _saleRepository.GetByIdAsync(id);

            if (existingSale == null)
            {
                _logger.LogError("Tentativa de atualizar uma venda com ID {SaleId} que não existe.", id);
                throw new Exception("Venda não encontrada para atualização.");
            }

            if (newSale == null)
            {
                _logger.LogError("Tentativa de atualizar uma venda com dados nulos.");
                throw new ArgumentNullException(nameof(newSale), "A nova venda não pode ser nula.");
            }

            _logger.LogInformation("Atualizando a venda com ID {SaleId}.", id);
            await _saleRepository.UpdateByIdAsync(id, newSale);
        }
    }
}
