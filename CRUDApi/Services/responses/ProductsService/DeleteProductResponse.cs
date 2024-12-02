using CRUDApi.Models;

namespace CRUDApi.Services.responses.ProductsService
{
    public class DeleteProductResponse
    {
        public bool IsError { get; set; } = false;
        public string? Error { get; set; }
    }
}
