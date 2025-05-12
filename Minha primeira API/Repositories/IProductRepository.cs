using Microsoft.AspNetCore.Mvc;
using Minha_primeira_API.Models;

namespace Minha_primeira_API.Repositories
{
    public interface IProductRepository
    {
        Task CreateProductAsync(Products products);
        Task<Products> GetByIdAsync(int id);
        Task UpdateByIdAsync(int id, Products newproductc);
        Task DeleteByIdAsync(Products product);
    }
}
