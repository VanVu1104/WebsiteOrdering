using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebsiteOrdering.Models
{
    [Table("NGUOIDUNG")]
    public class ApplicationUser : IdentityUser
    {
        [Column("HOTEN")]
        [StringLength(100)]
        public string? FullName { get; set; }

        [Column("NGAYSINH")]
        public DateOnly? BirthDate { get; set; }

        [Column("GIOITINH")]
        [StringLength(10)]
        public string? Gender { get; set; }

        public virtual ICollection<Datban> Datbans { get; set; } = new List<Datban>();

        public virtual ICollection<Donhang> Donhangs { get; set; } = new List<Donhang>();

        public virtual Chinhanh? IdchinhanhNavigation { get; set; }

    }
}
