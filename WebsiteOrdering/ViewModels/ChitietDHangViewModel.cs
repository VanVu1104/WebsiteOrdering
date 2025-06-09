using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebsiteOrdering.ViewModels
{
    [Table("CHITIETDONHANG")]
    [PrimaryKey(nameof(IDDONHANG), nameof(IDMONAN), nameof(IDMONAN2))]
    public class ChitietDHangViewModel
    {
        public string IDDONHANG { get; set; }
        public string IDMONAN { get; set; }
        public string IDMONAN2 { get; set; }
        public string IDSIZE { get; set; }
        public string IDDEBANH { get; set; }
        public int SOLUONG { get; set; }
        public int GIA { get; set; }
        public int TONGTIEN { get; set; }
        public string GHICHU { get; set; }
        public string KIEUPIZZA { get; set; }
        public virtual DonHangViewModel DonHang { get; set; }
        public virtual SizeViewModel Size { get; set; }
        public virtual DeBanhViewModel DeBanh { get; set; }
        public virtual ProductsViewModel Product { get; set; }
    }
}
