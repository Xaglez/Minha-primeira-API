using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Minha_primeira_API.Models;
using Minha_primeira_API.Services;

namespace Minha_primeira_API.Controller
{
    [ApiController]
    [Route("products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [Authorize]
        [HttpGet]
        [Route("listProducts")]
        public async Task<IActionResult> GetAllAsync()
        {
            var products = await _productService.GetAllAsync();
            if (products == null || !products.Any())
            {
                return NotFound("Nenhum produto encontrado.");
            }
            return Ok(products);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPost]
        [Route ("createProducts")]
        public async Task<IActionResult> CreateProductAsync([FromBody]Products products)
        {
            try
            {
                await _productService.CreateProductAsync(products);
                return Ok(products);
            }
            catch
            {
                return BadRequest("Produto vazio");
            }
            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var product = await _productService.GetByIdAsync(id);

            return Ok(product);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateByIdAsync(int id, [FromBody] Products newproductc)
        {
            await _productService.UpdateByIdAsync(id, newproductc);

            return NoContent();
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteByIdAsync(int id)
        {
            await _productService.DeleteByIdAsync(id);

            return NoContent();
        }
        
    }
}
