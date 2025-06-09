using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebsiteOrdering.Models.Entities;

[PrimaryKey("Idloaimonan", "Idsize")]
[Table("LISTGIASIZE")]
public partial class Listgiasize
{
    [Column("GIA")]
    public int Gia { get; set; }

    [Key]
    [Column("IDLOAIMONAN")]
    [StringLength(5)]
    [Unicode(false)]
    public string Idloaimonan { get; set; } = null!;

    [Key]
    [Column("IDSIZE")]
    [StringLength(5)]
    [Unicode(false)]
    public string Idsize { get; set; } = null!;

    [ForeignKey("Idloaimonan")]
    [InverseProperty("Listgiasizes")]
    public virtual Loaimonan IdloaimonanNavigation { get; set; } = null!;

    [ForeignKey("Idsize")]
    [InverseProperty("Listgiasizes")]
    public virtual Size IdsizeNavigation { get; set; } = null!;
}
