using System;
using System.Collections.Generic;

namespace WebsiteOrdering.Models;

public partial class Monan
{
    public string Idmonan { get; set; } = null!;

    public string Tenmonan { get; set; } = null!;

    public int Giamonan { get; set; }

    public string Anhmonan { get; set; } = null!;

    public string Mota { get; set; } = null!;

    public string Trangthaiman { get; set; } = null!;

    public string Idloaimonan { get; set; } = null!;

    public virtual ICollection<Chitietdonhang> Chitietdonhangs { get; set; } = new List<Chitietdonhang>();

    public virtual Loaimonan IdloaimonanNavigation { get; set; } = null!;
}
