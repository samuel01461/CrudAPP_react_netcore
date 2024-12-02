using CRUDApi.Models;

namespace CRUDApi.Repositories.interfaces
{
    public interface IUsers
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<IEnumerable<User>> GetUserAsync(int id);
        Task<IEnumerable<User>> GetUserByUsernameAsync(string username);
        Task<User> CreateUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
        Task DeleteUserAsync(int id);
    }
}
