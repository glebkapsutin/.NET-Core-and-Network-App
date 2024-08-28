using Microsoft.AspNetCore.Mvc;
using ProductApi.Models;
using ProductApi.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace ProductApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase // Наследуем от ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMemoryCache _cache;

        public ProductsController(ApplicationDbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            const string cacheKey = "products";
            if (!_cache.TryGetValue(cacheKey, out IEnumerable<Product> products))
            {
                products = await _context.Products.ToListAsync();
                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                };
                _cache.Set(cacheKey, products, cacheEntryOptions);
            }
            return Ok(products); // Исправлено
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
            try
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetProducts), new { id = product.Id }, product); // Исправлено
            }
            catch
            {
                return BadRequest("Error adding product"); // Исправлено
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound(); // Исправлено
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent(); // Исправлено
        }

        [HttpGet("csv")]
        public async Task<IActionResult> GetProductsAsCsv()
        {
            var products = await _context.Products.ToListAsync();
            var csv = new StringBuilder();
            csv.AppendLine("Id,Name,Price");
            foreach (var product in products)
            {
                csv.AppendLine($"{product.Id},{product.Name},{product.Price}");
            }
            var csvBytes = Encoding.UTF8.GetBytes(csv.ToString());
            var stream = new MemoryStream(csvBytes);
            return File(stream, "text/csv", "products.csv"); // Исправлено
        }

        [HttpGet("cache-stats")]
        public IActionResult GetCacheStats()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/cache-stats.txt");
            return PhysicalFile(filePath, "text/plain"); // Исправлено
        }
    }
}
