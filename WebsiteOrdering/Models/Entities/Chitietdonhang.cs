using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebsiteOrdering.Models.Entities;

[PrimaryKey("Iddonhang", "Idmonan", "Idmonan2")]
[Table("CHITIETDONHANG")]
public partial class Chitietdonhang
{
    [Column("SOLUONG")]
    public int Soluong { get; set; }

    [Column("GIA")]
    public int Gia { get; set; }

    [Column("TONGTIEN")]
    public int Tongtien { get; set; }

    [Column("GHICHU")]
    [StringLength(500)]
    public string? Ghichu { get; set; }

    [Column("KIEUPIZZA")]
    [StringLength(50)]
    public string? Kieupizza { get; set; }

    [Key]
    [Column("IDDONHANG")]
    [StringLength(5)]
    [Unicode(false)]
    public string Iddonhang { get; set; } = null!;

    [Column("IDDEBANH")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Iddebanh { get; set; }

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

    [Column("IDSIZE")]
    [StringLength(5)]
    [Unicode(false)]
    public string Idsize { get; set; } = null!;

    [ForeignKey("Iddebanh")]
    [InverseProperty("Chitietdonhangs")]
    public virtual Debanh? IddebanhNavigation { get; set; }

    [ForeignKey("Iddonhang")]
    [InverseProperty("Chitietdonhangs")]
    public virtual Donhang IddonhangNavigation { get; set; } = null!;

    [ForeignKey("Idsize")]
    [InverseProperty("Chitietdonhangs")]
    public virtual Size IdsizeNavigation { get; set; } = null!;

    [ForeignKey("Idmonan, Idmonan2")]
    [InverseProperty("Chitietdonhangs")]
    public virtual Monan Monan { get; set; } = null!;

    [ForeignKey("Iddonhang, Idmonan, Idmonan2")]
    [InverseProperty("Chitietdonhangs")]
    public virtual ICollection<Topping> Idtoppings { get; set; } = new List<Topping>();
}
