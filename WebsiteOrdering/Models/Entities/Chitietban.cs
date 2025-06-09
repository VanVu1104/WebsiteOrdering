using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebsiteOrdering.Models.Entities;

[PrimaryKey("Iddatban", "Idban")]
[Table("CHITIETBAN")]
public partial class Chitietban
{
    [Column("GIOVAO")]
    public TimeOnly Giovao { get; set; }

    [Column("GIORA")]
    public TimeOnly Giora { get; set; }

    [Key]
    [Column("IDDATBAN")]
    [StringLength(5)]
    [Unicode(false)]
    public string Iddatban { get; set; } = null!;

    [Key]
    [Column("IDBAN")]
    [StringLength(5)]
    [Unicode(false)]
    public string Idban { get; set; } = null!;

    [Column("TRANGTHAIBAN")]
    [StringLength(50)]
    public string Trangthaiban { get; set; } = null!;

    [ForeignKey("Idban")]
    [InverseProperty("Chitietbans")]
    public virtual Ban IdbanNavigation { get; set; } = null!;

    [ForeignKey("Iddatban")]
    [InverseProperty("Chitietbans")]
    public virtual Datban IddatbanNavigation { get; set; } = null!;
}
