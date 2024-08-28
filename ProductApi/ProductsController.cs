using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductApi.Models;
using ProductApi.Data;
using Microsoft.EntityFrameworkCore;


namespace ProductApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController
    {
        private readonly ApplicationDbContext _context;
        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }
       
        [HttpGet]
        public async Task<IEnumerable<Product>> GetProducts()
        {
            try
            {
                var products = await _context.Products.ToListAsync();
                return products; // Возвращаем список продуктов напрямую
            }
            catch
            {
                // Обработка ошибок, возможно, вернём пустой список или другое значение
                return new List<Product>(); // Возвращаем пустой список при ошибке
            }
        }
        [HttpPost]
        public async Task <Product> AddProducts(Product product)
        {
            try
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return product;
            }
            catch (System.Exception)
            {
                
                return null;
            }
        }
        [HttpDelete("{id}")]
        public async Task<bool> DeleteProducts(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return false;
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
 
        
    }



}