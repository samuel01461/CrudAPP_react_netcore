using CRUDApi.Models;

namespace CRUDApi.Services.responses.ProductsService
{
    public class GetProductsResponse
    {
        public bool IsError { get; set; } = false;
        public string? Error { get; set; }
        public IEnumerable<Product>? Products { get; set; }
    }
}
