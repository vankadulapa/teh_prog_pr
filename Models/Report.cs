using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace pr4.Models;

public partial class Report
{
    [Key]
    [Column("ReportID")]
    public int ReportId { get; set; }

    [Column("AdminID")]
    public int AdminId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime ReportDate { get; set; }

    public string ReportData { get; set; } = null!;

    [ForeignKey("AdminId")]
    [InverseProperty("Reports")]
    public virtual User Admin { get; set; } = null!;
}
