using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebsiteOrdering.Models.Entities;

[Table("KHACHHANG")]
public partial class Khachhang
{
    [Key]
    [Column("IDKH")]
    [StringLength(5)]
    [Unicode(false)]
    public string Idkh { get; set; } = null!;

    [Column("TENKH")]
    [StringLength(100)]
    public string Tenkh { get; set; } = null!;

    [Column("SĐTKH")]
    public int Sđtkh { get; set; }

    [Column("EMAILKH")]
    [StringLength(100)]
    public string Emailkh { get; set; } = null!;

    [Column("NGAYSINHKH")]
    public DateOnly? Ngaysinhkh { get; set; }

    [Column("DIACHIKH")]
    [StringLength(100)]
    public string? Diachikh { get; set; }

    [Column("GIOITINHKH")]
    [StringLength(10)]
    public string? Gioitinhkh { get; set; }

    [Column("MATKHAUKH")]
    [StringLength(50)]
    public string Matkhaukh { get; set; } = null!;

    [Column("THANHVIEN")]
    [StringLength(50)]
    public string? Thanhvien { get; set; }

    [InverseProperty("IdkhNavigation")]
    public virtual ICollection<Datban> Datbans { get; set; } = new List<Datban>();

    [InverseProperty("IdkhNavigation")]
    public virtual ICollection<Donhangonl> Donhangonls { get; set; } = new List<Donhangonl>();
}
