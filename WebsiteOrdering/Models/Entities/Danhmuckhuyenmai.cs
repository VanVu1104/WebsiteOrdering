using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebsiteOrdering.Models.Entities;

[Table("DANHMUCKHUYENMAI")]
public partial class Danhmuckhuyenmai
{
    [Key]
    [Column("IDKHUYENMAI")]
    [StringLength(5)]
    [Unicode(false)]
    public string Idkhuyenmai { get; set; } = null!;

    [Column("TENKHUYENMAI")]
    [StringLength(100)]
    public string Tenkhuyenmai { get; set; } = null!;

    [Column("NGAYAPDUNG")]
    public DateOnly Ngayapdung { get; set; }

    [Column("NGAYHETHAN")]
    public DateOnly Ngayhethan { get; set; }

    [Column("GIATRI")]
    public int Giatri { get; set; }

    [Column("MOTAKM")]
    [StringLength(500)]
    public string Motakm { get; set; } = null!;

    [InverseProperty("IdkhuyenmaiNavigation")]
    public virtual ICollection<Donhangonl> Donhangonls { get; set; } = new List<Donhangonl>();

    [InverseProperty("IdkhuyenmaiNavigation")]
    public virtual ICollection<Donhang> Donhangs { get; set; } = new List<Donhang>();
}
