using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebsiteOrdering.Models.Entities;

[PrimaryKey("Idmonan", "Idmonan2")]
[Table("MONAN")]
public partial class Monan
{
    [Key]
    [Column("IDMONAN")]
    [StringLength(5)]
    [Unicode(false)]
    public string Idmonan { get; set; } = null!;

    [Column("TENMONAN")]
    [StringLength(500)]
    public string Tenmonan { get; set; } = null!;

    [Column("MOTAMONAN")]
    [StringLength(500)]
    public string Motamonan { get; set; } = null!;

    [Column("GIACOBAN")]
    public int Giacoban { get; set; }

    [Column("ANHMONAN")]
    [StringLength(500)]
    [Unicode(false)]
    public string Anhmonan { get; set; } = null!;

    [Column("TRANGTHAI")]
    [StringLength(10)]
    public string Trangthai { get; set; } = null!;

    [Key]
    [Column("IDMONAN2")]
    [StringLength(5)]
    [Unicode(false)]
    public string Idmonan2 { get; set; } = null!;

    [Column("IDLOAIMONAN")]
    [StringLength(5)]
    [Unicode(false)]
    public string Idloaimonan { get; set; } = null!;

    [InverseProperty("Monan")]
    public virtual ICollection<Chitietdonhangonl> Chitietdonhangonls { get; set; } = new List<Chitietdonhangonl>();

    [InverseProperty("Monan")]
    public virtual ICollection<Chitietdonhang> Chitietdonhangs { get; set; } = new List<Chitietdonhang>();

    [ForeignKey("Idloaimonan")]
    [InverseProperty("Monans")]
    public virtual Loaimonan IdloaimonanNavigation { get; set; } = null!;
}
