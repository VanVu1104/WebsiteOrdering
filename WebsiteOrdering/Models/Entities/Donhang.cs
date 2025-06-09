using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebsiteOrdering.Models.Entities;

[Table("DONHANG")]
public partial class Donhang
{
    [Key]
    [Column("IDDONHANG")]
    [StringLength(5)]
    [Unicode(false)]
    public string Iddonhang { get; set; } = null!;

    [Column("NGAYDAT")]
    public DateOnly Ngaydat { get; set; }

    [Column("SONGUOI")]
    public int Songuoi { get; set; }

    [Column("TONGDH")]
    public int Tongdh { get; set; }

    [Column("TENKH")]
    [StringLength(50)]
    public string Tenkh { get; set; } = null!;

    [Column("PHUONGTHUCTHANHTOAN")]
    [StringLength(50)]
    public string Phuongthucthanhtoan { get; set; } = null!;

    [Column("IDCHINHANH")]
    [StringLength(5)]
    [Unicode(false)]
    public string Idchinhanh { get; set; } = null!;

    [Column("USERID")]
    [StringLength(450)]
    public string UserId { get; set; } = null!;

    [Column("IDDATBAN")]
    [StringLength(5)]
    [Unicode(false)]
    public string Iddatban { get; set; } = null!;

    [Column("IDKHUYENMAI")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Idkhuyenmai { get; set; }

    [InverseProperty("IddonhangNavigation")]
    public virtual ICollection<Chitietdonhang> Chitietdonhangs { get; set; } = new List<Chitietdonhang>();

    [ForeignKey("Idchinhanh")]
    [InverseProperty("Donhangs")]
    public virtual Chinhanh IdchinhanhNavigation { get; set; } = null!;

    [ForeignKey("Iddatban")]
    [InverseProperty("Donhangs")]
    public virtual Datban IddatbanNavigation { get; set; } = null!;

    [ForeignKey("Idkhuyenmai")]
    [InverseProperty("Donhangs")]
    public virtual Danhmuckhuyenmai? IdkhuyenmaiNavigation { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Donhangs")]
    public virtual ApplicationUser User { get; set; } = null!;
}
