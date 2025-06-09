using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebsiteOrdering.ViewModels
{
    [Table("CHINHANH")]
    [PrimaryKey(nameof(IDCHINHANH))]
    public class ChiNhanhViewModel
    {
        public string IDCHINHANH { get; set; }
        public string TENCHINHANH { get; set; }
        public string DIACHICN { get; set; }

    }
}
