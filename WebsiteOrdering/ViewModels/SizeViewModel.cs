using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebsiteOrdering.ViewModels
{
    [Table("SIZE")]

    [PrimaryKey(nameof(IDSIZE))]
    public class SizeViewModel
    {
      
        public required string IDSIZE { get; set; }

        public string TENSIZE { get; set; }
       
        public virtual ICollection<ListGiaSizeViewModel> ListGiaSize { get;set; }
    }
}
