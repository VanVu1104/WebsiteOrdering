using System;
using System.Collections.Generic;

namespace WebsiteOrdering.Models;

public partial class Chitietdonhang
{
    public string Ghichu { get; set; } = null!;

    public int Soluong { get; set; }

    public int Dongia { get; set; }

    public int Tongtiendh { get; set; }

    public string? Kieupizza { get; set; }

    public string? Idmonan2 { get; set; }

    public string Iddonhang { get; set; } = null!;

    public string Idmonan { get; set; } = null!;

    public string? Iddebanh { get; set; }

    public string? Idsize { get; set; }

    public virtual ICollection<Chitiettopping> Chitiettoppings { get; set; } = new List<Chitiettopping>();

    public virtual Debanh? IddebanhNavigation { get; set; }

    public virtual Donhang IddonhangNavigation { get; set; } = null!;

    public virtual Monan IdmonanNavigation { get; set; } = null!;

    public virtual Size? IdsizeNavigation { get; set; }
}
