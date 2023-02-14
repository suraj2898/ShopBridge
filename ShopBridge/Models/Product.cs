using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopBridge.Models;
public partial class Product
{
    [Required]
    public long ProductId { get; set; }

    [Required]
    [MaxLength(50)]
    [MinLength(1)]
    public string? Name { get; set; }

    public string? Description { get; set; }

    [Required]
    public decimal? Price { get; set; }

    [Required]
    public int? Quantity { get; set; }
}
