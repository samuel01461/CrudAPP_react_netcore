using System.ComponentModel.DataAnnotations;

namespace CRUDApi.Controllers.request
{
    public class RefreshTokenRequest
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Refresh_token { get; set; }
    }
}
