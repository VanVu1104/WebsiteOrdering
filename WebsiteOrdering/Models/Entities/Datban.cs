using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebsiteOrdering.Models.Entities;

[Table("DATBAN")]
public partial class Datban
{
    [Key]
    [Column("IDDATBAN")]
    [StringLength(5)]
    [Unicode(false)]
    public string Iddatban { get; set; } = null!;

    [Column("NGAYDAT")]
    public DateOnly Ngaydat { get; set; }

    [Column("GIOBATDAU")]
    public TimeOnly Giobatdau { get; set; }

    [Column("GIOKETTHUC")]
    public TimeOnly Gioketthuc { get; set; }

    [Column("SONGUOIDAT")]
    public int Songuoidat { get; set; }

    [Column("GHICHU")]
    [StringLength(500)]
    public string? Ghichu { get; set; }

    [Column("TRANGTHAIDATBAN")]
    [StringLength(50)]
    public string Trangthaidatban { get; set; } = null!;

    [Column("IDCHINHANH")]
    [StringLength(5)]
    [Unicode(false)]
    public string Idchinhanh { get; set; } = null!;

    [Column("USERID")]
    [StringLength(450)]
    public string UserId { get; set; } = null!;

    [InverseProperty("IddatbanNavigation")]
    public virtual ICollection<Chitietban> Chitietbans { get; set; } = new List<Chitietban>();

    [InverseProperty("IddatbanNavigation")]
    public virtual ICollection<Donhang> Donhangs { get; set; } = new List<Donhang>();

    [ForeignKey("Idchinhanh")]
    [InverseProperty("Datbans")]
    public virtual Chinhanh IdchinhanhNavigation { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("Datbans")]
    public virtual ApplicationUser User { get; set; } = null!;
}
