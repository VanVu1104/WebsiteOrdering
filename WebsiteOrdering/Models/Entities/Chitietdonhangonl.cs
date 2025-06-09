using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebsiteOrdering.Models.Entities;

[PrimaryKey("Iddonhangonl", "Idmonan", "Idmonan2")]
[Table("CHITIETDONHANGONL")]
public partial class Chitietdonhangonl
{
    [Column("SOLUONGDH")]
    public int Soluongdh { get; set; }

    [Column("GIADH")]
    public int Giadh { get; set; }

    [Column("TONGTIENDH")]
    public int Tongtiendh { get; set; }

    [Column("GHICHU")]
    [StringLength(500)]
    public string? Ghichu { get; set; }

    [Column("KIEUPIZZAONL")]
    [StringLength(50)]
    public string? Kieupizzaonl { get; set; }

    [Key]
    [Column("IDDONHANGONL")]
    [StringLength(5)]
    [Unicode(false)]
    public string Iddonhangonl { get; set; } = null!;

    [Column("IDDEBANH")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Iddebanh { get; set; }

    [Column("IDSIZE")]
    [StringLength(5)]
    [Unicode(false)]
    public string Idsize { get; set; } = null!;

    [Key]
    [Column("IDMONAN")]
    [StringLength(5)]
    [Unicode(false)]
    public string Idmonan { get; set; } = null!;

    [Key]
    [Column("IDMONAN2")]
    [StringLength(5)]
    [Unicode(false)]
    public string Idmonan2 { get; set; } = null!;

    [ForeignKey("Iddebanh")]
    [InverseProperty("Chitietdonhangonls")]
    public virtual Debanh? IddebanhNavigation { get; set; }

    [ForeignKey("Iddonhangonl")]
    [InverseProperty("Chitietdonhangonls")]
    public virtual Donhangonl IddonhangonlNavigation { get; set; } = null!;

    [ForeignKey("Idsize")]
    [InverseProperty("Chitietdonhangonls")]
    public virtual Size IdsizeNavigation { get; set; } = null!;

    [ForeignKey("Idmonan, Idmonan2")]
    [InverseProperty("Chitietdonhangonls")]
    public virtual Monan Monan { get; set; } = null!;

    [ForeignKey("Iddonhangonl, Idmonan, Idmonan2")]
    [InverseProperty("Chitietdonhangonls")]
    public virtual ICollection<Topping> Idtoppings { get; set; } = new List<Topping>();
}
