namespace CRUDApi.Services.responses.UsersService
{
    public class DeleteUserResponse
    {
        public bool IsError { get; set; } = false;
        public string? Error { get; set; }
    }
}
