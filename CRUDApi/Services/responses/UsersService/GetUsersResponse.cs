using CRUDApi.Models;

namespace CRUDApi.Services.responses.UsersService
{
    public class GetUsersResponse
    {
        public bool IsError { get; set; } = false;
        public string? Error { get; set; }
        public IEnumerable<User>? Users { get; set; }
    }
}
