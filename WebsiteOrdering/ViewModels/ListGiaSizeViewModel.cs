
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebsiteOrdering.ViewModels
{
    [Table("LISTGIASIZE")]
    [PrimaryKey(nameof(IDLOAIMONAN), nameof(IDSIZE))]
    public class ListGiaSizeViewModel
    {
        
        public string IDLOAIMONAN { get; set; }

        public string IDSIZE { get; set; }

        public int GIA { get; set; }

        [ForeignKey(nameof(IDLOAIMONAN))]
        public virtual CategoryViewModel LoaiMonAn { get; set; }
        [ForeignKey(nameof(IDSIZE))]
        public virtual SizeViewModel Size { get; set; }
    }

}
