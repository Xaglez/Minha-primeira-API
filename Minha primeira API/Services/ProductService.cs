using Minha_primeira_API.DTOs;
using Minha_primeira_API.Models;
using Minha_primeira_API.Repositories;


namespace Minha_primeira_API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IProductRepository productRepository, ILogger<ProductService> logger)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        public async Task CreateProductAsync(Products products)
        {
            _logger.LogInformation("Tentando criar um novo produto: {ProductName}", products?.Name);

            if (products == null)
            {
                _logger.LogError("Produto vazio ao tentar criar um novo produto.");
                throw new Exception("Produto vazio");
            }

            _logger.LogInformation("Produto válido, prosseguindo com a criação.");
            await _productRepository.CreateProductAsync(products);

        }

        public async Task DeleteByIdAsync(int id)
        {
            _logger.LogInformation("Tentando deletar o produto com ID: {ProductId}", id);

            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
            {
                _logger.LogError("Produto com ID {ProductId} não encontrado.", id);
                throw new Exception("Produto não encontrado");
            }

            _logger.LogInformation("Produto encontrado, prosseguindo com a deleção.");
            await _productRepository.DeleteByIdAsync(product);
        }

        public async Task<List<Products>> GetAllAsync()
        {
            _logger.LogInformation("Buscando todos os produtos.");
            var products = await _productRepository.GetAllAsync();
            return products;
        }

        public async Task<ProductDTO> GetByIdAsync(int id)
        {
            _logger.LogInformation("Buscando produto por ID: {ProductId}", id);
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
            {
                _logger.LogError("Produto com ID {ProductId} não encontrado.", id);
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

            _logger.LogInformation("Produto encontrado: {ProductName}", productResponse.Name);
            return productResponse;
        }

        public async Task UpdateByIdAsync(int id, Products newproductc)
        {
            _logger.LogInformation("Tentando atualizar o produto com ID: {ProductId}", id);
            if (newproductc == null)
            {
                _logger.LogError("Produto vazio ao tentar atualizar o produto com ID {ProductId}.", id);
                throw new Exception("Produto vazio");
            }

            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
            {
                _logger.LogError("Produto com ID {ProductId} não encontrado ao tentar atualizar.", id);
                throw new Exception("Produto não encontrado");
            }

            _logger.LogInformation("Produto encontrado, prosseguindo com a atualização.");
            await _productRepository.UpdateByIdAsync(id, newproductc);
        }       
    }
}
