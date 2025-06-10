using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebsiteOrdering.ViewModels
{
    [Table("LOAIMONAN")]
    [PrimaryKey(nameof(IDLOAIMONAN))]
    public class CategoryViewModel
    {
     
        [StringLength(5)]
        public required string IDLOAIMONAN { get; set; }

        [StringLength(50)]
        public string TENLOAIMONAN {  set; get; }

        [StringLength(5)]
        public string? IDLOAIMONANCHA { get; set; }

        public virtual ICollection<ProductsViewModel> Products { get; set; } = new List<ProductsViewModel>();
        public virtual ICollection<ToppingViewModel> Topping { get;set; } = new List<ToppingViewModel>();
    }
}
