using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace pr4.Models;

public partial class User
{
    [Key]
    [Column("UserID")]
    public int UserId { get; set; }

    [Column("RoleID")]
    public int RoleId { get; set; }

    [StringLength(100)]
    public string Login { get; set; } = null!;

    [StringLength(255)]
    public string Password { get; set; } = null!;

    [StringLength(150)]
    public string FullName { get; set; } = null!;

    [StringLength(20)]
    public string PhoneNum { get; set; } = null!;

    [StringLength(100)]
    public string Email { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    [InverseProperty("Client")]
    public virtual ICollection<Order> OrderClients { get; set; } = new List<Order>();

    [InverseProperty("Courier")]
    public virtual ICollection<Order> OrderCouriers { get; set; } = new List<Order>();

    [InverseProperty("Seller")]
    public virtual ICollection<Order> OrderSellers { get; set; } = new List<Order>();

    [InverseProperty("Admin")]
    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

    [ForeignKey("RoleId")]
    [InverseProperty("Users")]
    public virtual Role Role { get; set; } = null!;
}
