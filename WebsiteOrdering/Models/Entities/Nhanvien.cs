using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebsiteOrdering.Models.Entities;

[Table("NHANVIEN")]
public partial class Nhanvien
{
    [Key]
    [Column("IDNV")]
    [StringLength(5)]
    [Unicode(false)]
    public string Idnv { get; set; } = null!;

    [Column("TENNV")]
    [StringLength(100)]
    public string Tennv { get; set; } = null!;

    [Column("SĐTNV")]
    public int Sđtnv { get; set; }

    [Column("EMAILNV")]
    [StringLength(100)]
    public string Emailnv { get; set; } = null!;

    [Column("CHUCVU")]
    [StringLength(50)]
    public string Chucvu { get; set; } = null!;

    [Column("GIOITINHNV")]
    [StringLength(10)]
    public string Gioitinhnv { get; set; } = null!;

    [Column("NGAYSINHNV")]
    public DateOnly Ngaysinhnv { get; set; }

    [Column("MATKHAU")]
    [StringLength(50)]
    public string Matkhau { get; set; } = null!;

    [Column("IDCHINHANH")]
    [StringLength(5)]
    [Unicode(false)]
    public string Idchinhanh { get; set; } = null!;

    [InverseProperty("IdnvNavigation")]
    public virtual ICollection<Donhang> Donhangs { get; set; } = new List<Donhang>();

    [ForeignKey("Idchinhanh")]
    [InverseProperty("Nhanviens")]
    public virtual Chinhanh IdchinhanhNavigation { get; set; } = null!;
}
