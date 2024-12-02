namespace CRUDApi.Services.responses.AuthService
{
    public class TokenResponse
    {
        public bool IsError { get; set; } = false;
        public string? Error { get; set; }
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }

    }
}
