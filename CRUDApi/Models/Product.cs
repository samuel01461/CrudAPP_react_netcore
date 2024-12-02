using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRUDApi.Models;

public partial class Product
{
    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(50)]
    public string Name { get; set; } = null!;
    [Required]
    [StringLength(50)]
    public string Description { get; set; } = null!;
    [Required]
    public double Price { get; set; }
    [Required]
    public int? UserId { get; set; }

    public virtual User? User { get; set; }
}
