using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Minha_primeira_API.Data;
using Minha_primeira_API.Models;

namespace Minha_primeira_API.Controller
{
    [ApiController]
    [Route("v1")]
    public class ProductsController : ControllerBase
    {
        private readonly DbContext _dbContext;

        public ProductsController(AppDbContext appDb) {

            _dbContext = appDb;

        }

        [HttpPost]
        [Route ("products")]
        public async Task<IActionResult> CreateProductAsync([FromBody]Products products)
        {
            if (products == null)
            {
                return BadRequest ("Produto vazio");
            }

            _dbContext.Add(products);

            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var product = await _dbContext.Set<Products>().FindAsync(id);

            if ( product == null)
            {
                return BadRequest("O produto não existe!");
            }

            var productResponse = new
            {
                product.Id,
                product.Stock,
                product.Name,
                product.Category,
                product.Price
            };

            return Ok(productResponse);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateByIdAsync(int id, [FromBody] Products newproductc)
        {
            if (newproductc == null)
            {
                return BadRequest("Produto vazio");
            }

            var product = await _dbContext.Set<Products>().FindAsync(id);

            if (product == null)
            {
                return NotFound("Produto não encontrado");
            }

            product.Name = newproductc.Name;
            product.Stock = newproductc.Stock;
            product.Price = newproductc.Price;
            product.Category = newproductc.Category;

            await _dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteByIdAsync(int id)
        {
            var product = await _dbContext.Set<Products>().FindAsync(id);

            if (product == null)
            {
                return NotFound("Produto não encontrado");
            }

            _dbContext.Set<Products>().Remove(product);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
        
    }
}
