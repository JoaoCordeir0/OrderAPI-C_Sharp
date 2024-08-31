using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderAPI.Database;
using OrderAPI.Models;

namespace OrderAPI.Controllers
{
    [ApiController]
    [Route("api")]
    public class ProductController : ControllerBase
    {
        private readonly ApiDbContext _db;

        public ProductController(ApiDbContext apiDb)
        {
            _db = apiDb;
        }

        [HttpGet("product/list")]
        public async Task<ActionResult<IEnumerable<ProductModel>>> ListProducts()
        {
            return await _db.Product.ToListAsync();
        }

        [HttpGet("product/{id}")]
        public async Task<ActionResult<ProductModel>> GetProduct(int id)
        {
            var product = await _db.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound(new { message = $"Product ID: {id} not found!" });
            }
            return product;
        }

        [HttpPost("product/add")]
        public async Task<ActionResult<ProductModel>> AddProduct(ProductModel product)
        {
            _db.Product.Add(product);
            await _db.SaveChangesAsync();

            return Ok(new { message = "Product inserted successfully!" });
        }
    }
}
