using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebsiteOrdering.Models.Entities;

[Table("SIZE")]
public partial class Size
{
    [Key]
    [Column("IDSIZE")]
    [StringLength(5)]
    [Unicode(false)]
    public string Idsize { get; set; } = null!;

    [Column("TENSIZE")]
    [StringLength(50)]
    public string Tensize { get; set; } = null!;

    [InverseProperty("IdsizeNavigation")]
    public virtual ICollection<Chitietdonhangonl> Chitietdonhangonls { get; set; } = new List<Chitietdonhangonl>();

    [InverseProperty("IdsizeNavigation")]
    public virtual ICollection<Chitietdonhang> Chitietdonhangs { get; set; } = new List<Chitietdonhang>();

    [InverseProperty("IdsizeNavigation")]
    public virtual ICollection<Listgiasize> Listgiasizes { get; set; } = new List<Listgiasize>();
}
