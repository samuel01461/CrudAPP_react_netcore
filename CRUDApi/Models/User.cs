using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRUDApi.Models;

public partial class User
{
    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(50)]
    public string Username { get; set; } = null!;
    [Required]
    [StringLength(50)]
    public string Password { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<UsersRole> UsersRoles { get; set; } = new List<UsersRole>();
    public virtual ICollection<Tokens> Tokens { get; set; } = new List<Tokens>();
}
