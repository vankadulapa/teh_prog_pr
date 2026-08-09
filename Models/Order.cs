using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace pr4.Models;

public partial class Order
{
    [Key]
    [Column("OrderID")]
    public int OrderId { get; set; }

    [Column("ClientID")]
    public int ClientId { get; set; }

    [Column("SellerID")]
    public int? SellerId { get; set; }

    [Column("CourierID")]
    public int? CourierId { get; set; }

    [Column("StatusID")]
    public int StatusId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime OrderDate { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal TotalSum { get; set; }

    [ForeignKey("ClientId")]
    [InverseProperty("OrderClients")]
    public virtual User Client { get; set; } = null!;

    [ForeignKey("CourierId")]
    [InverseProperty("OrderCouriers")]
    public virtual User? Courier { get; set; }

    [InverseProperty("Order")]
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    [ForeignKey("SellerId")]
    [InverseProperty("OrderSellers")]
    public virtual User? Seller { get; set; }

    [ForeignKey("StatusId")]
    [InverseProperty("Orders")]
    public virtual OrderStatus Status { get; set; } = null!;
}
