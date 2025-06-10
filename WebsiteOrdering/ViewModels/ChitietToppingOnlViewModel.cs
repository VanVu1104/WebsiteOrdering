using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebsiteOrdering.ViewModels
{
    [Table("CHITIETTOPPINGONL")]
    [PrimaryKey(nameof(IDDONHANGONL), nameof(IDMONAN), nameof(IDMONAN2),nameof(IDTOPING))]
    public class ChitietToppingOnlViewModel
    {
        public string IDTOPING { get; set; }
        public string IDDONHANGONL { get; set; }
        public string IDMONAN {  get; set; }
        public string IDMONAN2 { get; set; }
        public virtual ChitietDHangOnlViewModel chitietdhangonl { get; set; }
        public virtual ToppingViewModel Topping { get; set; }
    }
}
