using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebsiteOrdering.Models.Entities;

[Table("DEBANH")]
public partial class Debanh
{
    [Key]
    [Column("IDDEBANH")]
    [StringLength(5)]
    [Unicode(false)]
    public string Iddebanh { get; set; } = null!;

    [Column("TENDEBANH")]
    [StringLength(50)]
    public string Tendebanh { get; set; } = null!;

    [Column("GIADEBANH")]
    public int Giadebanh { get; set; }

    [InverseProperty("IddebanhNavigation")]
    public virtual ICollection<Chitietdonhangonl> Chitietdonhangonls { get; set; } = new List<Chitietdonhangonl>();

    [InverseProperty("IddebanhNavigation")]
    public virtual ICollection<Chitietdonhang> Chitietdonhangs { get; set; } = new List<Chitietdonhang>();
}
