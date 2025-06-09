using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebsiteOrdering.Models;
using WebsiteOrdering.Models.Entities;

namespace WebsiteOrdering.Data;

public partial class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
    {
    }

    public virtual DbSet<Ban> Bans { get; set; }

    public virtual DbSet<Chinhanh> Chinhanhs { get; set; }

    public virtual DbSet<Chitietban> Chitietbans { get; set; }

    public virtual DbSet<Chitietdonhang> Chitietdonhangs { get; set; }

    public virtual DbSet<Chitietdonhangonl> Chitietdonhangonls { get; set; }

    public virtual DbSet<Danhmuckhuyenmai> Danhmuckhuyenmais { get; set; }

    public virtual DbSet<Datban> Datbans { get; set; }

    public virtual DbSet<Debanh> Debanhs { get; set; }

    public virtual DbSet<Donhang> Donhangs { get; set; }

    public virtual DbSet<Donhangonl> Donhangonls { get; set; }

    public virtual DbSet<Listgiasize> Listgiasizes { get; set; }

    public virtual DbSet<Loaimonan> Loaimonans { get; set; }

    public virtual DbSet<Monan> Monans { get; set; }

    public virtual DbSet<Size> Sizes { get; set; }

    public virtual DbSet<Topping> Toppings { get; set; }
    public virtual DbSet<Location> Locations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Pizza;Trusted_Connection=True;MultipleActiveResultSets=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
       
        modelBuilder.Entity<Ban>(entity =>
        {
            entity.HasKey(e => e.Idban).HasName("PK__BAN__9367225EC3E6D028");

            entity.Property(e => e.Idban).IsFixedLength();
            entity.Property(e => e.Idchinhanh).IsFixedLength();

            entity.HasOne(d => d.IdchinhanhNavigation).WithMany(p => p.Bans)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BAN__IDCHINHANH__6477ECF3");
        });

        modelBuilder.Entity<Chinhanh>(entity =>
        {
            entity.HasKey(e => e.Idchinhanh).HasName("PK__CHINHANH__5F20FC4041DF698B");

            entity.Property(e => e.Idchinhanh).IsFixedLength();
        });

        modelBuilder.Entity<Chitietban>(entity =>
        {
            entity.HasKey(e => new { e.Iddatban, e.Idban }).HasName("PK__CHITIETB__27F2122C9119BD2E");

            entity.Property(e => e.Iddatban).IsFixedLength();
            entity.Property(e => e.Idban).IsFixedLength();

            entity.HasOne(d => d.IdbanNavigation).WithMany(p => p.Chitietbans)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CHITIETBA__IDBAN__76969D2E");

            entity.HasOne(d => d.IddatbanNavigation).WithMany(p => p.Chitietbans)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CHITIETBA__IDDAT__75A278F5");
        });

        modelBuilder.Entity<Chitietdonhang>(entity =>
        {
            entity.HasKey(e => new { e.Iddonhang, e.Idmonan, e.Idmonan2 }).HasName("PK__CHITIETD__8EFDF8BDABAA408C");

            entity.Property(e => e.Iddonhang).IsFixedLength();
            entity.Property(e => e.Idmonan).IsFixedLength();
            entity.Property(e => e.Idmonan2).IsFixedLength();
            entity.Property(e => e.Iddebanh).IsFixedLength();
            entity.Property(e => e.Idsize).IsFixedLength();

            entity.HasOne(d => d.IddebanhNavigation).WithMany(p => p.Chitietdonhangs).HasConstraintName("FK__CHITIETDO__IDDEB__0A9D95DB");

            entity.HasOne(d => d.IddonhangNavigation).WithMany(p => p.Chitietdonhangs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CHITIETDO__IDDON__09A971A2");

            entity.HasOne(d => d.IdsizeNavigation).WithMany(p => p.Chitietdonhangs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CHITIETDO__IDSIZ__0C85DE4D");

            entity.HasOne(d => d.Monan).WithMany(p => p.Chitietdonhangs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CHITIETDONHANG__0B91BA14");
        });

        modelBuilder.Entity<Chitietdonhangonl>(entity =>
        {
            entity.HasKey(e => new { e.Iddonhangonl, e.Idmonan, e.Idmonan2 }).HasName("PK__CHITIETD__D7486150E300646B");

            entity.Property(e => e.Iddonhangonl).IsFixedLength();
            entity.Property(e => e.Idmonan).IsFixedLength();
            entity.Property(e => e.Idmonan2).IsFixedLength();
            entity.Property(e => e.Iddebanh).IsFixedLength();
            entity.Property(e => e.Idsize).IsFixedLength();

            entity.HasOne(d => d.IddebanhNavigation).WithMany(p => p.Chitietdonhangonls).HasConstraintName("FK__CHITIETDO__IDDEB__10566F31");

            entity.HasOne(d => d.IddonhangonlNavigation).WithMany(p => p.Chitietdonhangonls)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CHITIETDO__IDDON__0F624AF8");

            entity.HasOne(d => d.IdsizeNavigation).WithMany(p => p.Chitietdonhangonls)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CHITIETDO__IDSIZ__114A936A");

            entity.HasOne(d => d.Monan).WithMany(p => p.Chitietdonhangonls)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CHITIETDONHANGON__123EB7A3");
        });

        modelBuilder.Entity<Danhmuckhuyenmai>(entity =>
        {
            entity.HasKey(e => e.Idkhuyenmai).HasName("PK__DANHMUCK__9E055897C118320B");

            entity.Property(e => e.Idkhuyenmai).IsFixedLength();
        });

        modelBuilder.Entity<Datban>(entity =>
        {
            entity.HasKey(e => e.Iddatban).HasName("PK__DATBAN__DEC46009C032B7D1");

            entity.Property(e => e.Iddatban).IsFixedLength();
            entity.Property(e => e.Idchinhanh).IsFixedLength();
            entity.Property(e => e.UserId).HasMaxLength(450);

            entity.HasOne(d => d.IdchinhanhNavigation).WithMany(p => p.Datbans)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DATBAN__IDCHINHA__60A75C0F");

            entity.HasOne(d => d.User).WithMany(p => p.Datbans)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DATBAN__USERID__619B8048");
        });

        modelBuilder.Entity<Debanh>(entity =>
        {
            entity.HasKey(e => e.Iddebanh).HasName("PK__DEBANH__555F23FFB140968E");

            entity.Property(e => e.Iddebanh).IsFixedLength();
        });

        modelBuilder.Entity<Donhang>(entity =>
        {
            entity.HasKey(e => e.Iddonhang).HasName("PK__DONHANG__F59FA8B118A5D193");

            entity.Property(e => e.Iddonhang).IsFixedLength();
            entity.Property(e => e.Idchinhanh).IsFixedLength();
            entity.Property(e => e.Iddatban).IsFixedLength();
            entity.Property(e => e.Idkhuyenmai).IsFixedLength();
            entity.Property(e => e.UserId).HasMaxLength(450);

            entity.HasOne(d => d.IdchinhanhNavigation).WithMany(p => p.Donhangs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DONHANG__IDCHINH__7C4F7684");

            entity.HasOne(d => d.IddatbanNavigation).WithMany(p => p.Donhangs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DONHANG__IDDATBA__7E37BEF6");

            entity.HasOne(d => d.IdkhuyenmaiNavigation).WithMany(p => p.Donhangs).HasConstraintName("FK__DONHANG__IDKHUYE__7F2BE32F");

            entity.HasOne(d => d.User).WithMany(p => p.Donhangs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DONHANG__USERID__7D439ABD");
        });

        modelBuilder.Entity<Donhangonl>(entity =>
        {
            entity.HasKey(e => e.Iddonhangonl).HasName("PK__DONHANGO__AC2A315C15B12C03");

            entity.Property(e => e.Iddonhangonl).IsFixedLength();
            entity.Property(e => e.Idchinhanh).IsFixedLength();
            entity.Property(e => e.UserId).HasMaxLength(450);
            entity.Property(e => e.Idkhuyenmai).IsFixedLength();

            entity.HasOne(d => d.IdchinhanhNavigation).WithMany(p => p.Donhangonls)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DONHANGON__IDCHI__02FC7413");

            entity.HasOne(d => d.User).WithMany(p => p.Donhangonls)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DONHANGONL__USERID__02084FDA");

            entity.HasOne(d => d.IdkhuyenmaiNavigation).WithMany(p => p.Donhangonls).HasConstraintName("FK__DONHANGON__IDKHU__03F0984C");
        });

        modelBuilder.Entity<Listgiasize>(entity =>
        {
            entity.HasKey(e => new { e.Idloaimonan, e.Idsize }).HasName("PK__LISTGIAS__93A480291F324B05");

            entity.Property(e => e.Idloaimonan).IsFixedLength();
            entity.Property(e => e.Idsize).IsFixedLength();

            entity.HasOne(d => d.IdloaimonanNavigation).WithMany(p => p.Listgiasizes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LISTGIASI__IDLOA__71D1E811");

            entity.HasOne(d => d.IdsizeNavigation).WithMany(p => p.Listgiasizes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LISTGIASI__IDSIZ__72C60C4A");
        });

        modelBuilder.Entity<Loaimonan>(entity =>
        {
            entity.HasKey(e => e.Idloaimonan).HasName("PK__LOAIMONA__6B7E94ED8043D23A");

            entity.Property(e => e.Idloaimonan).IsFixedLength();
        });
        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ID__6B7E94ED8043D11B");

            entity.Property(e => e.Id).IsFixedLength();
        });
        modelBuilder.Entity<Monan>(entity =>
        {
            entity.HasKey(e => new { e.Idmonan, e.Idmonan2 }).HasName("PK__MONAN__B62500C613AC53C3");

            entity.Property(e => e.Idmonan).IsFixedLength();
            entity.Property(e => e.Idmonan2).IsFixedLength();
            entity.Property(e => e.Idloaimonan).IsFixedLength();

            entity.HasOne(d => d.IdloaimonanNavigation).WithMany(p => p.Monans)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MONAN__IDLOAIMON__06CD04F7");
        });

        modelBuilder.Entity<Size>(entity =>
        {
            entity.HasKey(e => e.Idsize).HasName("PK__SIZE__8DA14C4E58CC03AA");

            entity.Property(e => e.Idsize).IsFixedLength();
        });

        modelBuilder.Entity<Topping>(entity =>
        {
            entity.HasKey(e => e.Idtopping).HasName("PK__TOPPING__B17F5B459FE8393F");

            entity.Property(e => e.Idtopping).IsFixedLength();
            entity.Property(e => e.Idloaimonan).IsFixedLength();

            entity.HasOne(d => d.IdloaimonanNavigation).WithMany(p => p.Toppings)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TOPPING__IDLOAIM__6B24EA82");

            entity.HasMany(d => d.Chitietdonhangonls).WithMany(p => p.Idtoppings)
                .UsingEntity<Dictionary<string, object>>(
                    "Chitiettoppingonl",
                    r => r.HasOne<Chitietdonhangonl>().WithMany()
                        .HasForeignKey("Iddonhangonl", "Idmonan", "Idmonan2")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__CHITIETTOPPINGON__19DFD96B"),
                    l => l.HasOne<Topping>().WithMany()
                        .HasForeignKey("Idtopping")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__CHITIETTO__IDTOP__18EBB532"),
                    j =>
                    {
                        j.HasKey("Idtopping", "Iddonhangonl", "Idmonan", "Idmonan2").HasName("PK__CHITIETT__AC0BDD50F7A79B3B");
                        j.ToTable("CHITIETTOPPINGONL");
                        j.IndexerProperty<string>("Idtopping")
                            .HasMaxLength(5)
                            .IsUnicode(false)
                            .IsFixedLength()
                            .HasColumnName("IDTOPPING");
                        j.IndexerProperty<string>("Iddonhangonl")
                            .HasMaxLength(5)
                            .IsUnicode(false)
                            .IsFixedLength()
                            .HasColumnName("IDDONHANGONL");
                        j.IndexerProperty<string>("Idmonan")
                            .HasMaxLength(5)
                            .IsUnicode(false)
                            .IsFixedLength()
                            .HasColumnName("IDMONAN");
                        j.IndexerProperty<string>("Idmonan2")
                            .HasMaxLength(5)
                            .IsUnicode(false)
                            .IsFixedLength()
                            .HasColumnName("IDMONAN2");
                    });

            entity.HasMany(d => d.Chitietdonhangs).WithMany(p => p.Idtoppings)
                .UsingEntity<Dictionary<string, object>>(
                    "Chitiettopping",
                    r => r.HasOne<Chitietdonhang>().WithMany()
                        .HasForeignKey("Iddonhang", "Idmonan", "Idmonan2")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__CHITIETTOPPING__160F4887"),
                    l => l.HasOne<Topping>().WithMany()
                        .HasForeignKey("Idtopping")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__CHITIETTO__IDTOP__151B244E"),
                    j =>
                    {
                        j.HasKey("Idtopping", "Iddonhang", "Idmonan", "Idmonan2").HasName("PK__CHITIETT__799084CEE74352D4");
                        j.ToTable("CHITIETTOPPING");
                        j.IndexerProperty<string>("Idtopping")
                            .HasMaxLength(5)
                            .IsUnicode(false)
                            .IsFixedLength()
                            .HasColumnName("IDTOPPING");
                        j.IndexerProperty<string>("Iddonhang")
                            .HasMaxLength(5)
                            .IsUnicode(false)
                            .IsFixedLength()
                            .HasColumnName("IDDONHANG");
                        j.IndexerProperty<string>("Idmonan")
                            .HasMaxLength(5)
                            .IsUnicode(false)
                            .IsFixedLength()
                            .HasColumnName("IDMONAN");
                        j.IndexerProperty<string>("Idmonan2")
                            .HasMaxLength(5)
                            .IsUnicode(false)
                            .IsFixedLength()
                            .HasColumnName("IDMONAN2");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
