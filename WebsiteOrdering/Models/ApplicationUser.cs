using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebsiteOrdering.Models.Entities;

namespace WebsiteOrdering.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Column("FullName")]
        public string? FullName { get; set; } = null;
        [Column("NGAYSINH")]
        public DateOnly? BirthDate { get; set; } = null;

        [Column("GIOITINH")]
        [StringLength(10)]
        public string? Gender { get; set; } = null;
        [Column("IDCHINHANH")]
        [StringLength(5)]
        [Unicode(false)]
        public string? Idchinhanh { get; set; } = null;
        [ForeignKey("Idchinhanh")]
        public virtual Chinhanh? IdchinhanhNavigation { get; set; } = null!;
        [InverseProperty("User")]
        public virtual ICollection<Datban>? Datbans { get; set; } = new List<Datban>();

        [InverseProperty("User")]
        public virtual ICollection<Donhangonl>? Donhangonls { get; set; } = new List<Donhangonl>();

        [InverseProperty("User")]
        public virtual ICollection<Donhang>? Donhangs { get; set; } = new List<Donhang>();
    }
}
