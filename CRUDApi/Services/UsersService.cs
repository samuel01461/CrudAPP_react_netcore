using CRUDApi.Models;
using CRUDApi.Repositories.interfaces;
using CRUDApi.Services.interfaces;
using CRUDApi.Services.responses.UsersService;

namespace CRUDApi.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsers _usersRepository;
        public UsersService(IUsers usersRepository)
        {
            _usersRepository = usersRepository;
        }
        public async Task<GetUsersResponse> GetUsersAsync()
        {
            var users = await _usersRepository.GetUsersAsync();

            if (users != null && users.Count() > 0)
            {
                return new GetUsersResponse { IsError = false, Error = null, Users = users };
            }
            else
            {
                return new GetUsersResponse { IsError = true, Error = "Users not found", Users = [] };
            }
        }
        public async Task<GetUserResponse> GetUserAsync(int id)
        {
            var user = await _usersRepository.GetUserAsync(id);

            if (user != null && user.Count() > 0)
            {
                return new GetUserResponse { IsError = false, Error = null, User = user.First() };
            }
            else
            {
                return new GetUserResponse { IsError = true, Error = "User not found", User = null };
            }
        }

        public async Task<GetUserResponse> GetUserByUsernameAsync(string username)
        {
            var user = await _usersRepository.GetUserByUsernameAsync(username);

            if (user != null && user.Any())
            {
                return new GetUserResponse { IsError = false, Error = null, User = user.First() };
            }
            else
            {
                return new GetUserResponse { IsError = true, Error = "User not found", User = null };
            }
        }
        public async Task<CreateUserResponse> CreateUserAsync(User user)
        {
            try
            {
                var new_user = await _usersRepository.CreateUserAsync(user);
                return new CreateUserResponse { IsError = false, Error = null, User = new_user };
            }
            catch (Exception e)
            {
                return new CreateUserResponse { IsError = true, Error = e.Message, User = null };
            }
        }
        public async Task<UpdateUserResponse> UpdateUserAsync(User user)
        {
            try
            {
                var update_product = await _usersRepository.UpdateUserAsync(user);
                return new UpdateUserResponse { IsError = false, Error = null, User = user };
            }
            catch (Exception e)
            {
                return new UpdateUserResponse { IsError = true, Error = e.Message, User = null };
            }
        }
        public async Task<DeleteUserResponse> DeleteUserAsync(int id)
        {
            try
            {
                await _usersRepository.DeleteUserAsync(id);
                return new DeleteUserResponse { IsError = false, Error = null };
            }
            catch (Exception e)
            {
                return new DeleteUserResponse { IsError = true, Error = e.Message };
            }
        }
    }
}
