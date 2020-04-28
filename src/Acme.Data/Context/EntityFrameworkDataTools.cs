using Acme.Data.DataModels;
using Acme.Data.DataModels.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Acme.Data.Context
{
    public class EntityFrameworkDataTools : DbContext
    {
        private readonly string _connectionString;

        public EntityFrameworkDataTools(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DbSet<LogEntryDataModel> Logs { get; set; }
        public DbSet<ProductCategoryDataModel> ProductCategories { get; set; }
        public DbSet<ProductReviewDataModel> ProductReviews { get; set; }
        public DbSet<ProductDataModel> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("AcmeDb0001"); // todo : replace with sql server or similar
            //optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductDataModel>()
            .HasIndex(x => x.Price)
            .HasName("IX_Products_Price");

            modelBuilder.Entity<ProductDataModel>()
            .HasIndex(x => x.DeletedOn)
            .HasName("IX_Products_DeletedOn");

            modelBuilder.Entity<ProductDataModel>()
            .HasIndex(x => x.Sku)
            .HasName("IX_Products_Sku");

            modelBuilder.Entity<ProductDataModel>()
            .HasIndex(x => x.CategoryId)
            .HasName("IX_Products_CategoryId");

            modelBuilder.Entity<ProductReviewDataModel>()
            .HasIndex(x => x.ProductId)
            .HasName("IX_ProductReviews_ProductId");

            modelBuilder.Entity<ProductReviewDataModel>()
            .HasIndex(x => x.Score)
            .HasName("IX_ProductReviews_Score");
        }        
    }
}