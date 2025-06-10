using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebsiteOrdering.ViewModels
{
    [Table("DONHANG")]

    [PrimaryKey(nameof(IDDONHANG))]
    public class DonHangViewModel
    {
        public string IDDONHANG { get; set; }
        public DateTime NGAYDAT { get; set; }
        public int SONGUOI { get; set; }
        public int TONGDH {  get; set; }
        public string TENKH { get; set; }
        public string PHUONGTHUCTHANHTOAN { get; set; }
        public string IDCHINHANH { get; set; }
        public string IDNV {  get; set; }
        public string IDDATBAN {  get; set; }
        public string IDKHUYENMAI { get; set; }

        public virtual ICollection<ChitietDHangViewModel> chitietdh { get; set; } = new List<ChitietDHangViewModel>();
        public virtual ChiNhanhViewModel chinhanh { get; set; }

    }
}
