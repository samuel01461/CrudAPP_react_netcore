using CRUDApi.Models;
using CRUDApi.Services.interfaces;
using CRUDApi.Repositories.interfaces;
using Microsoft.EntityFrameworkCore;
using CRUDApi.Services.responses.ProductsService;

namespace CRUDApi.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IProducts _productsRepository;
        public ProductsService(IProducts productsRepository) {
            _productsRepository = productsRepository;
        }
        public async Task<GetProductsResponse> GetProductsAsync()
        {
            var products = await _productsRepository.GetProductsAsync();

            if (products != null && products.Count() > 0)
            {
                return new GetProductsResponse { IsError = false, Error = null, Products = products };
            }
            else
            {
                return new GetProductsResponse { IsError = true, Error = "Products not found", Products = [] };
            }
        }

        public async Task<GetProductsResponse> GetMyProductsAsync(int userId)
        {
            var products = await _productsRepository.GetMyProductsAsync(userId);

            if (products != null && products.Count() > 0)
            {
                return new GetProductsResponse { IsError = false, Error = null, Products = products };
            }
            else
            {
                return new GetProductsResponse { IsError = true, Error = "Products not found", Products = [] };
            }
        }
        public async Task<GetProductResponse> GetProductAsync(int id)
        {
            var product = await _productsRepository.GetProductAsync(id);

            if (product != null && product.Count() > 0)
            {
                return new GetProductResponse { IsError = false, Error = null, Product = product.First() };
            }
            else
            {
                return new GetProductResponse { IsError = true, Error = "Product not found", Product = null };
            }
        }
        public async Task<CreateProductResponse> CreateProductAsync(Product product)
        {
            try
            {
                var new_product = await _productsRepository.CreateProductAsync(product);
                return new CreateProductResponse { IsError = false, Error = null, Product = new_product };
            }
            catch (Exception e)
            {
                return new CreateProductResponse { IsError = true, Error = e.Message, Product = null };
            }
        }
        public async Task<UpdateUserResponse> UpdateProductAsync(Product product)
        {
            try
            {
                var update_product = await _productsRepository.UpdateProductAsync(product);
                return new UpdateUserResponse { IsError = false, Error = null, Product = product };
            }
            catch (Exception e)
            {
                return new UpdateUserResponse { IsError = true, Error = e.Message, Product = null };
            }
        }
        public async Task<DeleteProductResponse> DeleteProductAsync(int id)
        {
            try
            {
                await _productsRepository.DeleteProductAsync(id);
                return new DeleteProductResponse { IsError = false, Error = null };
            }
            catch (Exception e)
            {
                return new DeleteProductResponse { IsError = true, Error = e.Message };
            }
        }
    }
}
