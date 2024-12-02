using CRUDApi.Models;

namespace CRUDApi.Repositories.interfaces
{
    public interface IProducts
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<IEnumerable<Product>> GetMyProductsAsync(int userId);
        Task<IEnumerable<Product>> GetProductAsync(int id);
        Task<Product> CreateProductAsync(Product product);
        Task<Product> UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);
    }
}
