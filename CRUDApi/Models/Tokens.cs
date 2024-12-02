using System.ComponentModel.DataAnnotations;

namespace CRUDApi.Models
{
    public class Tokens
    {
        [Key]
        public int Id { get; set; }
        public string? Refresh_token { get; set; }
        public int UserId { get; set; }
        public byte Used { get; set; }
        public DateTime Expires { get; set; }
        public virtual User User { get; set; }
    }
}
