using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Minha_primeira_API.Data;
using Minha_primeira_API.Models;

namespace Minha_primeira_API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _dbContext;
        
        public ProductRepository(AppDbContext appDb)
        {
            _dbContext = appDb;
        }

        public async Task CreateProductAsync(Products products)
        {
            _dbContext.Add(products);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(Products product)
        {
            _dbContext.Set<Products>().Remove(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Products> GetByIdAsync(int id)
        {

            var product = await _dbContext.Products.FindAsync(id);

            if (product == null)
            {
                throw new Exception("Não foi possivél encontrar");
            }

            return (product);
        }

        public async Task UpdateByIdAsync(int id, Products newproductc)
        {
            var product = await GetByIdAsync(id);

            product.Name = newproductc.Name;
            product.Stock = newproductc.Stock;
            product.Price = newproductc.Price;
            product.Category = newproductc.Category;

            await _dbContext.SaveChangesAsync();
        }
    }
}
