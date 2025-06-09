using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebsiteOrdering.ViewModels
{
    [Table("CHITIETTOPPING")]
    [PrimaryKey(nameof(IDDONHANG), nameof(IDMONAN), nameof(IDMONAN2), nameof(IDTOPING))]
    public class ChitietToppingViewModel
    {
        public string IDTOPING { get; set; }
        public string IDDONHANG { get; set; }
        public string IDMONAN { get; set; }
        public string IDMONAN2 { get; set; }
        public virtual ChitietDHangViewModel chitietdhang { get; set; }
        public virtual ToppingViewModel Topping { get; set; }
    }
}
