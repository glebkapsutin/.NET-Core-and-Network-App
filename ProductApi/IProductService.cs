using ProductApi.Models;

namespace ProductApi.Services
{
    public interface IProductService
    {   
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<Product> AddProductAsync(Product product);
        Task<bool> DeleteProductAsync(int id);
    }
}