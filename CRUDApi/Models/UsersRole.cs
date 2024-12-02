using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRUDApi.Models;

public partial class UsersRole
{
    [Key]
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? RolId { get; set; }

    public virtual User? User { get; set; }
}
