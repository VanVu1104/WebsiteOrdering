using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebsiteOrdering.Models.Entities;

[Table("CHINHANH")]
public partial class Chinhanh
{
    [Key]
    [Column("IDCHINHANH")]
    [StringLength(5)]
    [Unicode(false)]
    public string Idchinhanh { get; set; } = null!;

    [Column("TENCHINHANH")]
    [StringLength(100)]
    public string Tenchinhanh { get; set; } = null!;

    [Column("DIACHICN")]
    [StringLength(100)]
    public string Diachicn { get; set; } = null!;

    [InverseProperty("IdchinhanhNavigation")]
    public virtual ICollection<Ban> Bans { get; set; } = new List<Ban>();

    [InverseProperty("IdchinhanhNavigation")]
    public virtual ICollection<Datban> Datbans { get; set; } = new List<Datban>();

    [InverseProperty("IdchinhanhNavigation")]
    public virtual ICollection<Donhangonl> Donhangonls { get; set; } = new List<Donhangonl>();

    [InverseProperty("IdchinhanhNavigation")]
    public virtual ICollection<Donhang> Donhangs { get; set; } = new List<Donhang>();

    [InverseProperty("IdchinhanhNavigation")]
    public virtual ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
}
