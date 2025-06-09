using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebsiteOrdering.ViewModels
{
    [Table("DEBANH")]
    [PrimaryKey(nameof(IDDEBANH))]
    public class DeBanhViewModel
    {
        [Key]
        public string IDDEBANH { get; set; }
   
        public string TENDEBANH { get; set; }
     
        public int GIADEBANH { get; set; }

    }
}
