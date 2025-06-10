using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebsiteOrdering.ViewModels
{
    [Table("DONHANG")]
   
    [PrimaryKey(nameof(IDDONHANGONL))]
    public class DonHangOnlViewModel
    {
       
        public string IDDONHANGONL { get; set; }
        public string DIACHI { get; set; }
        public string TRANGTHAI { get; set; }
        public int TONGTIEN { get; set; }
        public DateTime NGAYDATDON {  get; set; }
        public string PTTTONL { get; set; }
        public int TIENSHIP {  get; set; }
        public string IDKH {  get; set; }
        public string IDCHINHANH { get; set; }
        public string IDKHUYENMAI { get; set; }

        public virtual ICollection<ChitietDHangOnlViewModel> chitietdhonl { get; set; } = new List<ChitietDHangOnlViewModel>();
        public virtual ChiNhanhViewModel chinhanh {  get; set; }
    }
}
