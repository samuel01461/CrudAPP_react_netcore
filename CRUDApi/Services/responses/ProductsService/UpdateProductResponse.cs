using CRUDApi.Models;

namespace CRUDApi.Services.responses.ProductsService
{
    public class UpdateUserResponse
    {
        public bool IsError { get; set; } = false;
        public string? Error { get; set; }
        public Product? Product { get; set; }
    }
}
