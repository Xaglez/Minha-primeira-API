using Minha_primeira_API.DTOs;
using Minha_primeira_API.Models;

namespace Minha_primeira_API.Services
{
    public interface IProductService
    {
        Task CreateProductAsync(Products products);
        Task<ProductDTO> GetByIdAsync(int id);
        Task UpdateByIdAsync(int id, Products newproductc);
        Task DeleteByIdAsync(int id);
    }
}
