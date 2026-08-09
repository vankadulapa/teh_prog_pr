using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace pr4.Models;

[Table("Brand")]
public partial class Brand
{
    [Key]
    [Column("BrandID")]
    public int BrandId { get; set; }

    [StringLength(100)]
    public string BrandName { get; set; } = null!;

    [InverseProperty("Brand")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
