using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebsiteOrdering.Models.Entities;

[Table("DONHANGONL")]
public partial class Donhangonl
{
    [Key]
    [Column("IDDONHANGONL")]
    [StringLength(5)]
    [Unicode(false)]
    public string Iddonhangonl { get; set; } = null!;

    [Column("DIACHI")]
    [StringLength(100)]
    public string Diachi { get; set; } = null!;

    [Column("TRANGTHAI")]
    [StringLength(50)]
    public string Trangthai { get; set; } = null!;

    [Column("TONGTIEN")]
    public int Tongtien { get; set; }

    [Column("NGAYDATDON")]
    public DateOnly Ngaydatdon { get; set; }

    [Column("PTTTONL")]
    [StringLength(50)]
    public string Ptttonl { get; set; } = null!;

    [Column("TIENSHIP")]
    public int Tienship { get; set; }


    [Column("USERID")]
    [StringLength(450)]
    public string UserId { get; set; } = null!;

    [Column("IDCHINHANH")]
    [StringLength(5)]
    [Unicode(false)]
    public string Idchinhanh { get; set; } = null!;

    [Column("IDKHUYENMAI")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Idkhuyenmai { get; set; }

    [InverseProperty("IddonhangonlNavigation")]
    public virtual ICollection<Chitietdonhangonl> Chitietdonhangonls { get; set; } = new List<Chitietdonhangonl>();

    [ForeignKey("Idchinhanh")]
    [InverseProperty("Donhangonls")]
    public virtual Chinhanh IdchinhanhNavigation { get; set; } = null!;


    [ForeignKey("UserId")]
    [InverseProperty("Donhangonls")]
    public virtual ApplicationUser User { get; set; } = null!;

    [ForeignKey("Idkhuyenmai")]
    [InverseProperty("Donhangonls")]
    public virtual Danhmuckhuyenmai? IdkhuyenmaiNavigation { get; set; }
}
