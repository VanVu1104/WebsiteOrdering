using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace WebsiteOrdering.ViewModels
{
    [Table("MONAN")]
    [PrimaryKey(nameof(IDMONAN), nameof(IDMONAN2))]
  
    public class ProductsViewModel
    {

        public string IDMONAN { get; set; }

        public string IDMONAN2 { get; set; }

        public string? TENMONAN { get; set; }
 
        public string? MOTAMONAN { get; set; }
   
        public int GIACOBAN {  get; set; }
     
        public string? ANHMONAN { get; set; }
       
        public string? TRANGTHAI {  get; set; }

    
        public string IDLoaiMonAn { get; set; }
        [NotMapped]
        public List<SanPhamViewModel> PizzaGhep { get; set; } = new List<SanPhamViewModel>();
       
        [ForeignKey(nameof(IDLoaiMonAn))]
        public virtual CategoryViewModel Category { get; set; }
     
        public List<ListGiaSizeViewModel> ListGiaSizes { get; set; }
        // public List<DeBanhViewModel> DeBanh { get; set; }
        [NotMapped]
        public List<DeBanhViewModel> DeBanh { get; set; } = new();
        public List<ToppingViewModel> Toppings { get; set; }
        [NotMapped]
        public int TongTien { get; set; } =0;
        [NotMapped]
        public int SoLuong { get; set; } = 1;

    }
  
    public class SanPhamViewModel
    {
        public string IDMONAN { get; set; }
        public string IDMONAN2 { get; set; }
        public string TENMONAN { get; set; }
        public int GIACOBAN { get; set; }
        public string ANHMONAN { get; set; }
    }
}
