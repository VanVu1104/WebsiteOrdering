using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebsiteOrdering.Models.Entities;

[Table("BAN")]
public partial class Ban
{
    [Key]
    [Column("IDBAN")]
    [StringLength(5)]
    [Unicode(false)]
    public string Idban { get; set; } = null!;

    [Column("TENBAN")]
    [StringLength(50)]
    public string Tenban { get; set; } = null!;

    [Column("SONGUOI")]
    public int Songuoi { get; set; }

    [Column("KHUVUC")]
    [StringLength(500)]
    public string? Khuvuc { get; set; }

    [Column("IDCHINHANH")]
    [StringLength(5)]
    [Unicode(false)]
    public string Idchinhanh { get; set; } = null!;

    [InverseProperty("IdbanNavigation")]
    public virtual ICollection<Chitietban> Chitietbans { get; set; } = new List<Chitietban>();

    [ForeignKey("Idchinhanh")]
    [InverseProperty("Bans")]
    public virtual Chinhanh IdchinhanhNavigation { get; set; } = null!;
}
