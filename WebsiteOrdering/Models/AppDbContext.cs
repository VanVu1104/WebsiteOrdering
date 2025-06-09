using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebsiteOrdering.ViewModels;

namespace WebsiteOrdering.Models
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext()
        {

        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public  DbSet<ProductsViewModel> SanPhams { get; set; }
        public  DbSet<CategoryViewModel> Categories { get; set; }
        public  DbSet<SizeViewModel> Sizes { get; set; }
        public  DbSet<ListGiaSizeViewModel> ListGiaSizes { get; set; }
        public DbSet<ToppingViewModel> Topping { get; set; }
        public DbSet<ChitietDHangOnlViewModel> CtDHOnl { get; set; }
        public DbSet<ChitietDHangViewModel> ctdh {  get; set; }
        public DbSet<ChitietToppingOnlViewModel> cttoppingonl { get; set; }
        public DbSet<ChitietToppingViewModel> cttopping {  get; set; }
        public DbSet<ChiNhanhViewModel> chinhanh    { get; set; }
        public DbSet<DonHangOnlViewModel> dhangonl  { get; set; }
        public DbSet<DonHangViewModel> dhang { get; set; }
        public DbSet<DeBanhViewModel> debanh { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Bảng chi tiết đơn hàng onl
            modelBuilder.Entity<ChitietDHangOnlViewModel>()
                .ToTable("CHITIETDONHANGONL")
                .HasKey(c => new { c.IDDONHANGONL, c.IDMONAN, c.IDMONAN2 });

            modelBuilder.Entity<ChitietDHangOnlViewModel>()
                .HasOne(c => c.Product)
                .WithMany() // hoặc .WithMany(p => p.ChiTietDHOnl) nếu có navigation ngược
                .HasForeignKey(c => new { c.IDMONAN, c.IDMONAN2 });

            modelBuilder.Entity<ChitietDHangOnlViewModel>()
                .HasOne(c => c.DonHangOnl)
                .WithMany()
                .HasForeignKey(c => c.IDDONHANGONL);

            modelBuilder.Entity<ChitietDHangOnlViewModel>()
                .HasOne(c => c.Size)
                .WithMany()
                .HasForeignKey(c => c.IDSIZE);

            modelBuilder.Entity<ChitietDHangOnlViewModel>()
                .HasOne(c => c.DeBanh)
                .WithMany()
                .HasForeignKey(c => c.IDDEBANH);

            //Bảng chi tiết đơn hàng 
            modelBuilder.Entity<ChitietDHangViewModel>()
               .ToTable("CHITIETDONHANG")
               .HasKey(c => new { c.IDDONHANG, c.IDMONAN, c.IDMONAN2 });

            modelBuilder.Entity<ChitietDHangViewModel>()
                .HasOne(c => c.Product)
                .WithMany() // hoặc .WithMany(p => p.ChiTietDHOnl) nếu có navigation ngược
                .HasForeignKey(c => new { c.IDMONAN, c.IDMONAN2 });

            modelBuilder.Entity<ChitietDHangViewModel>()
                .HasOne(c => c.DonHang)
                .WithMany()
                .HasForeignKey(c => c.IDDONHANG);

            modelBuilder.Entity<ChitietDHangViewModel>()
                .HasOne(c => c.Size)
                .WithMany()
                .HasForeignKey(c => c.IDSIZE);

            modelBuilder.Entity<ChitietDHangViewModel>()
                .HasOne(c => c.DeBanh)
                .WithMany()
                .HasForeignKey(c => c.IDDEBANH);


            //Bảng size
            modelBuilder.Entity<SizeViewModel>()
                .ToTable("SIZE")
                .HasKey(s => new { s.IDSIZE });

            //Bảng list giá size
            modelBuilder.Entity<ListGiaSizeViewModel>()
                .ToTable("LISTGIASIZE")
                .HasKey(x => new { x.IDLOAIMONAN, x.IDSIZE });

            modelBuilder.Entity<ListGiaSizeViewModel>()
                .HasOne(x => x.LoaiMonAn)
                .WithMany()
                .HasForeignKey(x => x.IDLOAIMONAN);

            modelBuilder.Entity<ListGiaSizeViewModel>()
                .HasOne(x => x.Size)
                .WithMany()
                .HasForeignKey(x => x.IDSIZE);

            //Bảng loại món ăn
            modelBuilder.Entity<CategoryViewModel>()
                .ToTable("LOAIMONAN")
                .HasKey(l => new { l.IDLOAIMONAN });

            //Bảng đế bánh
            modelBuilder.Entity<DeBanhViewModel>()
                .ToTable("DEBANH")
                .HasKey(b => b.IDDEBANH);
         

            //Bảng sản phẩm
            modelBuilder.Entity<ProductsViewModel>()
                .ToTable("MONAN")
                .HasKey(sp => new {sp.IDMONAN,sp.IDMONAN2});
            modelBuilder.Entity<ProductsViewModel>()
                .HasOne(sp => sp.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(sp => sp.IDLoaiMonAn);

            //Bảng topping
            modelBuilder.Entity<ToppingViewModel>()
                .ToTable("TOPPING")
                .HasKey(t => new { t.IDTOPPING });

            modelBuilder.Entity<ToppingViewModel>()
                .HasOne(t => t.IdLoaiMonAn)
                .WithMany()
                .HasForeignKey(t => t.IDLOAIMONAN);

            //Bảng chi nhánh
            modelBuilder.Entity<ChiNhanhViewModel>()
                .ToTable("CHINHANH")
                .HasKey(cn => cn.IDCHINHANH);
            
            //Bảng đơn hàng online
            modelBuilder.Entity<DonHangOnlViewModel>()
                .ToTable("DONHANGONL")
                .HasKey(dho => dho.IDDONHANGONL);
            modelBuilder.Entity<DonHangOnlViewModel>()
                .HasOne(dho => dho.chinhanh)
                .WithMany()
                .HasForeignKey(dho => dho.IDCHINHANH);

            //Bảng đơn hàng 
            modelBuilder.Entity<DonHangViewModel>()
                .ToTable("DONHANG")
                .HasKey(dh => dh.IDDONHANG);
            modelBuilder.Entity<DonHangViewModel>()
                .HasOne(dh => dh.chinhanh)
                .WithMany()
                .HasForeignKey(dh => dh.IDCHINHANH);

            //Bảng chi tiết topping online
            modelBuilder.Entity<ChitietToppingOnlViewModel>()
                .ToTable("CHITIETTOPPINGONL")
                .HasKey(ct => new { ct.IDTOPING, ct.IDDONHANGONL, ct.IDMONAN, ct.IDMONAN2 });
            modelBuilder.Entity<ChitietToppingOnlViewModel>()
                .HasOne(ct =>ct.chitietdhangonl)
                .WithMany()
                .HasForeignKey(ct => new {ct.IDDONHANGONL,ct.IDMONAN,ct.IDMONAN2});
            modelBuilder.Entity<ChitietToppingOnlViewModel>()
                .HasOne(ct => ct.Topping)
                .WithMany()
                .HasForeignKey(ct =>ct.IDTOPING);

            //Bảng chi tiết topping 
            modelBuilder.Entity<ChitietToppingViewModel>()
               .ToTable("CHITIETTOPPING")
               .HasKey(ct => new { ct.IDTOPING, ct.IDDONHANG, ct.IDMONAN, ct.IDMONAN2 });
            modelBuilder.Entity<ChitietToppingViewModel>()
                .HasOne(ct => ct.chitietdhang)
                .WithMany()
                .HasForeignKey(ct => new { ct.IDDONHANG, ct.IDMONAN, ct.IDMONAN2 });
            modelBuilder.Entity<ChitietToppingViewModel>()
                .HasOne(ct => ct.Topping)
                .WithMany()
                .HasForeignKey(ct => ct.IDTOPING);
        }

    }
}
