using CRUDApi.Models;
using CRUDApi.Services.responses.ProductsService;

namespace CRUDApi.Services.interfaces
{
    public interface IProductsService
    {
        Task<GetProductsResponse> GetProductsAsync();
        Task<GetProductResponse> GetProductAsync(int id);
        Task<GetProductsResponse> GetMyProductsAsync(int userId);
        Task<CreateProductResponse> CreateProductAsync(Product product);
        Task<UpdateUserResponse> UpdateProductAsync(Product product);
        Task<DeleteProductResponse> DeleteProductAsync(int id);
    }
}
