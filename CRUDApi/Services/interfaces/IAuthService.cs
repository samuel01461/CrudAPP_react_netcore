using CRUDApi.Models;
using CRUDApi.Services.responses.AuthService;

namespace CRUDApi.Services.interfaces
{
    public interface IAuthService
    {
        Task<LoginResponse> Login(string username, string password);
        Task<TokenResponse> GenerateToken(User user);
        Task<bool> CheckRefreshToken(int userId, string refreshToken);
    }
}
