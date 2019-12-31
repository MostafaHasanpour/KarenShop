using KarenShop.Api.Domains;
using KarenShop.Api.Domains.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KarenShop.Api.Infrastructures
{
    public class KarenShopDbContext : DbContext
    {
        public KarenShopDbContext(DbContextOptions<KarenShopDbContext> options) : base(options)
        {

        }

        public DbSet<ShopUser> ShopUsers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductPictureAddress> ProductPictureAddress { get; set; }
        public DbSet<ProductColor> ProductColors { get; set; }
        public DbSet<ProductPrice> ProductPrices { get; set; }
        public DbSet<ProductSize> ProductSizes { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ShopUser>()
                .Property(t => t.Email)
                    .IsRequired();
            modelBuilder.Entity<ShopUser>()
                .HasIndex(t => t.Email)
                    .IsUnique();
            modelBuilder.Entity<ShopUser>()
                .Property(t => t.PhoneNumber)
                    .IsRequired();
            modelBuilder.Entity<ShopUser>()
                .HasIndex(t => t.PhoneNumber)
                    .IsUnique();
        }
    }
}