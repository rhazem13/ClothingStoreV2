using System;
using System.Collections.Generic;
using ClothingStoreV2.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ClothingStoreV2.Models
{
    public partial class ClothingStoreContext : ClothingStore_IdentityContext
    {
       

        public ClothingStoreContext(DbContextOptions<ClothingStoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Item> Items { get; set; } = null!;
        public virtual DbSet<Purchase> Purchases { get; set; } = null!;
        public virtual DbSet<PurchaseItem> PurchaseItems { get; set; } = null!;
        public virtual DbSet<UserDatum> UserData { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                //optionsBuilder.UseSqlServer("Server=HAZEM;Database=ClothingStore;Trusted_Connection=True;");
                //optionsBuilder.UseSqlServer(
                //    "Server=tcp:rhazem13.database.windows.net,1433;Initial Catalog=ClothingStore;Persist Security Info=False;User ID=rhazem13;Password=Hazm1102001;MultipleActiveResultSets=False;Trusted_Connection=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;Integrated Security=False;");
                optionsBuilder.UseSqlServer(
                    "Server=ec2-54-82-205-3.compute-1.amazonaws.com;Port=5432;Database=dc10jd3qvoa1ar;User Id=dbverjjecbcifw;Password=bce0cef6fe5b61734d2808dd8057389075099ae536008ce70a37db6e2ce87d5a;sslmode=Require;TrustServerCertificate=True;"
                );
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryLabel).HasMaxLength(64);
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.Property(e => e.ItemLabel).HasMaxLength(50);

                entity.Property(e => e.PhotoPath).HasMaxLength(260);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__Items__CategoryI__286302EC");
            });

            modelBuilder.Entity<Purchase>(entity =>
            {
               // entity.Property(e => e.PurchaseDate).HasColumnType("datetime");

                entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Purchases)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Purchases__UserI__2B3F6F97");
            });

            modelBuilder.Entity<PurchaseItem>(entity =>
            {
                entity.HasKey(e => new { e.PurchaseId, e.ItemId })
                    .HasName("PK__Purchase__CC2D838651A53020");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.PurchaseItems)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PurchaseI__ItemI__2F10007B");

                entity.HasOne(d => d.Purchase)
                    .WithMany(p => p.PurchaseItems)
                    .HasForeignKey(d => d.PurchaseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PurchaseI__Purch__2E1BDC42");
            });

            modelBuilder.Entity<UserDatum>(entity =>
            {
                entity.Property(e => e.Address1).HasMaxLength(95);

                entity.Property(e => e.Address2).HasMaxLength(95);

                entity.Property(e => e.City).HasMaxLength(189);

                entity.Property(e => e.Email).HasMaxLength(254);

                entity.Property(e => e.FirstName).HasMaxLength(64);

                entity.Property(e => e.IdentityId).HasMaxLength(450);

                entity.Property(e => e.LastName).HasMaxLength(64);

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.Property(e => e.ZipCode).HasMaxLength(18);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
