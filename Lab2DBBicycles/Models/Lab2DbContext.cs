using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Lab2DBBicycles
{
    public partial class Lab2DbContext : DbContext
    {
        public Lab2DbContext()
        {
        }

        public Lab2DbContext(DbContextOptions<Lab2DbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bicycle> Bicycles { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<Store> Stores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server= DESKTOP-7P6VKRI; Database=Lab2Db; Trusted_Connection=True; ");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Bicycle>(entity =>
            {
                entity.Property(e => e.Info).HasMaxLength(1000);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Bicycles)
                    .HasForeignKey(d => d.BrandId)
                    .OnDelete(DeleteBehavior.ClientCascade)
                    .HasConstraintName("FK_Bicycles_Brands");
            });

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.Property(e => e.Info).HasMaxLength(1000);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Brands)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientCascade)
                    .HasConstraintName("FK_Brands_Countries");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.HasOne(d => d.Bicycle)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(d => d.BicycleId)
                    .OnDelete(DeleteBehavior.ClientCascade)
                    .HasConstraintName("FK_Sales_Bicycles");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientCascade)
                    .HasConstraintName("FK_Sales_Stores");
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.Property(e => e.Info).HasMaxLength(1000);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
