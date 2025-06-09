using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebsiteOrdering.Models.Entities;

[Table("LOAIMONAN")]
public partial class Loaimonan
{
    [Key]
    [Column("IDLOAIMONAN")]
    [StringLength(5)]
    [Unicode(false)]
    public string Idloaimonan { get; set; } = null!;

    [Column("TENLOAIMONAN")]
    [StringLength(50)]
    public string Tenloaimonan { get; set; } = null!;

    [InverseProperty("IdloaimonanNavigation")]
    public virtual ICollection<Listgiasize> Listgiasizes { get; set; } = new List<Listgiasize>();

    [InverseProperty("IdloaimonanNavigation")]
    public virtual ICollection<Monan> Monans { get; set; } = new List<Monan>();

    [InverseProperty("IdloaimonanNavigation")]
    public virtual ICollection<Topping> Toppings { get; set; } = new List<Topping>();
}
