using Microsoft.EntityFrameworkCore;
using Minha_primeira_API.DTOs;
using Minha_primeira_API.Models;
using Minha_primeira_API.Repositories;
using System.Data;

namespace Minha_primeira_API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {

            _productRepository = productRepository;
        }

        public async Task CreateProductAsync(Products products)
        {
            if (products == null)
            {
                throw new Exception("Produto vazio");
            }

            await _productRepository.CreateProductAsync(products);

        }

        public async Task DeleteByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
            {
                throw new Exception("Produto não encontrado");
            }

            await _productRepository.DeleteByIdAsync(product);
        }

        public async Task<ProductDTO> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
            {
                throw new Exception("O produto não existe!");
            }

            var productResponse = new ProductDTO
            {
                Id = product.Id,
                Stock = product.Stock,
                Name = product.Name,
                Category = product.Category,
                Price = product.Price
            };

            return productResponse;
        }

        public async Task UpdateByIdAsync(int id, Products newproductc)
        {
            if (newproductc == null)
            {
                throw new Exception("Produto vazio");
            }

            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
            {
                throw new Exception("Produto não encontrado");
            }

            await _productRepository.UpdateByIdAsync(id, newproductc);
        }
    }
}
