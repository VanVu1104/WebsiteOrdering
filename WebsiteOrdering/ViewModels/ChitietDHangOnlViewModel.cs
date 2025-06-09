using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace WebsiteOrdering.ViewModels
{
    [Table("CHITIETDONHANGONL")]
    [PrimaryKey(nameof(IDDONHANGONL), nameof(IDMONAN), nameof(IDMONAN2))]
    public class ChitietDHangOnlViewModel
    {
       
        public string IDDONHANGONL { get; set; }
       
        public string IDMONAN { get; set; }
    
        public string IDMONAN2 { get; set; }
        public string IDSIZE { get; set; }
        public string IDDEBANH { get; set; }
        public int SOLUONGDH { get; set; }
        public int GIADH { get; set; }
        public int TONGTIENDH { get; set; }
        public string GHICHU { get; set; }
        public string KIEUPIZZAONL { get; set; }
        public virtual DonHangOnlViewModel DonHangOnl { get; set; }
        public virtual SizeViewModel Size { get; set; }
        public virtual DeBanhViewModel DeBanh { get; set; }
        public virtual ProductsViewModel Product { get; set; }

    }
}
