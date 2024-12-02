using CRUDApi.Models;
using CRUDApi.Services.responses;
using CRUDApi.Services.responses.UsersService;

namespace CRUDApi.Services.interfaces
{
    public interface IUsersService
    {
        Task<GetUsersResponse> GetUsersAsync();
        Task<GetUserResponse> GetUserAsync(int id);
        Task<GetUserResponse> GetUserByUsernameAsync(string username);
        Task<CreateUserResponse> CreateUserAsync(User user);
        Task<UpdateUserResponse> UpdateUserAsync(User user);
        Task<DeleteUserResponse> DeleteUserAsync(int id);
    }
}
