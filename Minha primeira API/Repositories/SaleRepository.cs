using Microsoft.EntityFrameworkCore;
using Minha_primeira_API.Data;
using Minha_primeira_API.DTOs;
using Minha_primeira_API.Models;

namespace Minha_primeira_API.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly AppDbContext _appDbContext;

        public SaleRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task CreateSaleAsync(Venda sale)
        {
             _appDbContext.Add(sale);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var sale = await GetByIdAsync(id);

            _appDbContext
                .Set<Venda>()
                .Remove(sale);

            await _appDbContext.SaveChangesAsync();
        }

        public async Task<List<Venda>> GetAllAsync()
        {
            var sales = await _appDbContext.Vendas
                .ToListAsync();

            return sales ?? throw new Exception("Não foi possível encontrar vendas.");
        }

        public  async Task<List<Venda>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var ListVendas = await _appDbContext.Vendas
                .Where(v => v.DataVenda >= startDate && v.DataVenda <= endDate)
                .ToListAsync();

            return ListVendas ?? throw new Exception("Nenhuma venda encontrada no intervalo de datas especificado.");
        }

        public async Task<Venda> GetByIdAsync(int id)
        {
            return await _appDbContext.Vendas
                .FindAsync(id) ?? throw new Exception("Venda não encontrada.");
        }

        public async Task<List<Venda>> GetByProductIdAsync(int productId)
        {
            var sales = await _appDbContext.Vendas.Where(v => v.ProductId == productId)
                .ToListAsync();


            return sales ?? throw new Exception("Nenhuma venda encontrada para o produto especificado.");
        }

        public async Task<List<Venda>> GetByUserIdAsync(int userId)
        {
            var sales = await _appDbContext.Vendas
                .Where(v => v.UserId == userId)
                .ToListAsync();

            return sales ?? throw new Exception("Nenhuma venda encontrada para o usuário especificado.");
        }

        public async Task UpdateByIdAsync(int id, Venda newSale)
        {
            var oldSale = _appDbContext.Vendas.Find(id);

            if (oldSale == null)
            {
                throw new Exception("Venda não encontrada.");
            }

            oldSale.DataVenda = newSale.DataVenda;
            oldSale.UserId = newSale.UserId;

            _appDbContext.Vendas.Update(newSale);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
