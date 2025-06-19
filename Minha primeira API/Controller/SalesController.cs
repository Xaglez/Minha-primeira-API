using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Minha_primeira_API.Models;
using Minha_primeira_API.Services;
using Minha_primeira_API.DTOs;

namespace Minha_primeira_API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly ISaleService _salesService;
        private readonly IUsersService _usersService;
        private readonly IProductService _productService;

        public SalesController(ISaleService salesService, IUsersService usersService, IProductService productService)
        {
            _salesService = salesService;
            _usersService = usersService;
            _productService = productService;
        }

        [Authorize]
        [HttpGet("listSales")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var sales = await _salesService.GetAllAsync();
                return Ok(sales);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("createSale")]
        public async Task<IActionResult> CreateSaleAsync([FromBody] CreateSaleDTO dto)
        {
            var userId = await _usersService.GetByIdAsync(dto.UserId);
            var productId = await _productService.GetByIdAsync(dto.ProductId);

            if (userId == null || productId == null)
            {
                return BadRequest("Usuário ou produto inválido.");
            }

            try
            {
                var sale = new Venda
                {
                    UserId = dto.UserId,
                    ProductId = dto.ProductId,
                    DataVenda = DateTime.UtcNow,
                    Amount = dto.Amount,
                    TotalValue = dto.TotalValue
                };

                await _salesService.CreateSaleAsync(sale);
                return Ok(sale);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("ById/{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var sale = await _salesService.GetByIdAsync(id);
                return Ok(sale);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteByIdAsync(int id)
        {
            try
            {
                await _salesService.DeleteByIdAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("byDateRange")]
        public async Task<IActionResult> GetByDateRangeAsync([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            try
            {
                var sales = await _salesService.GetByDateRangeAsync(startDate, endDate);
                return Ok(sales);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("byProductId/{productId}")]
        public async Task<IActionResult> GetByProductIdAsync(int productId)
        {
            try
            {
                var sales = await _salesService.GetByProductIdAsync(productId);
                return Ok(sales);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }

        [Authorize]
        [HttpGet("byUserId/{userId}")]
        public async Task<IActionResult> GetByUserIdAsync(int userId)
        {
            try
            {
                var sales = await _salesService.GetByUserIdAsync(userId);
                return Ok(sales);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
