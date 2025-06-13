using System;
using System.Collections.Generic;

namespace WebsiteOrdering.Models;

public partial class Topping
{
    public string Idtopping { get; set; } = null!;

    public string Tentopping { get; set; } = null!;

    public int Giatopping { get; set; }

    public string Idloaimonan { get; set; } = null!;

    public virtual Chitiettopping? Chitiettopping { get; set; }

    public virtual Loaimonan IdloaimonanNavigation { get; set; } = null!;
}
