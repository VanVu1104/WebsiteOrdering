using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebsiteOrdering.Models.Entities;

[Table("TOPPING")]
public partial class Topping
{
    [Key]
    [Column("IDTOPPING")]
    [StringLength(5)]
    [Unicode(false)]
    public string Idtopping { get; set; } = null!;

    [Column("TENTOPPING")]
    [StringLength(50)]
    public string Tentopping { get; set; } = null!;

    [Column("GIATOPPING")]
    public int Giatopping { get; set; }

    [Column("IDLOAIMONAN")]
    [StringLength(5)]
    [Unicode(false)]
    public string Idloaimonan { get; set; } = null!;

    [ForeignKey("Idloaimonan")]
    [InverseProperty("Toppings")]
    public virtual Loaimonan IdloaimonanNavigation { get; set; } = null!;

    [ForeignKey("Idtopping")]
    [InverseProperty("Idtoppings")]
    public virtual ICollection<Chitietdonhangonl> Chitietdonhangonls { get; set; } = new List<Chitietdonhangonl>();

    [ForeignKey("Idtopping")]
    [InverseProperty("Idtoppings")]
    public virtual ICollection<Chitietdonhang> Chitietdonhangs { get; set; } = new List<Chitietdonhang>();
}
