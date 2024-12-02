using CRUDApi.Models;

namespace CRUDApi.Services.responses.AuthService
{
    public class LoginResponse
    {
        public bool IsError { get; set; } = false;
        public string? Error { get; set; }
        public User? User { get; set; }
    }
}
