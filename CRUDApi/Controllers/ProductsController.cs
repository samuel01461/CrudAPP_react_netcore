using CRUDApi.Models;
using CRUDApi.Repositories;
using CRUDApi.Services.interfaces;
using CRUDApi.Services.responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CRUDApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;
        public ProductsController(IProductsService productsService) {
            _productsService = productsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {
            return Ok(await _productsService.GetProductsAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductAsync(int id)
        {
            return Ok(await _productsService.GetProductAsync(id));
        }

        [HttpGet]
        [Route("GetMyProducts")]
        public async Task<IActionResult> GetMyProducts()
        {
            var user = User;
            var userId = int.Parse(User.FindFirst(ClaimTypes.Sid).Value);
            return Ok(await _productsService.GetMyProductsAsync(userId));
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductAsync([FromBody] Product product)
        {
            if (!ModelState.IsValid) {
                return BadRequest();
            }

            return Ok(await _productsService.CreateProductAsync(product));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProductAsync([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(await _productsService.UpdateProductAsync(product));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductAsync(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.Sid).Value);

            var product = await _productsService.GetProductAsync(id);
            if (product.Product == null)
            {
                return NotFound();
            }

            if (product.Product.UserId != userId )
            {
                return Unauthorized();
            }
            return Ok(await _productsService.DeleteProductAsync(id));
        }
    }
}
