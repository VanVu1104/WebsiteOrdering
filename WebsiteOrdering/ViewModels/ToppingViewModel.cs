using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebsiteOrdering.ViewModels
{
    [Table("TOPPING")]

    [PrimaryKey(nameof(IDTOPPING))]
    public class ToppingViewModel
    {
       
        public string IDTOPPING {  get; set; }
       
        public string TENTOPPING { get; set; }
       
        public int GIATOPPING { get; set; }

        public string IDLOAIMONAN { get; set; }
       
        public virtual CategoryViewModel IdLoaiMonAn { get; set; }
    }
}
