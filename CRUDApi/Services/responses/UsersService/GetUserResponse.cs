using CRUDApi.Models;

namespace CRUDApi.Services.responses.UsersService
{
    public class GetUserResponse
    {
        public bool IsError { get; set; } = false;
        public string? Error { get; set; }
        public User? User { get; set; }
    }
}
