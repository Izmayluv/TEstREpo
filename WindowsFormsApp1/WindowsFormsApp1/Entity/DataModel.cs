using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace WindowsFormsApp1.Entity
{
    public partial class DataModel : DbContext
    {
        public DataModel()
            : base("name=DataModel")
        {
        }

        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Manufacturer> Manufacturer { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Provider> Provider { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasMany(e => e.Product)
                .WithOptional(e => e.Category)
                .HasForeignKey(e => e.productCategory);

            modelBuilder.Entity<Manufacturer>()
                .HasMany(e => e.Product)
                .WithOptional(e => e.Manufacturer)
                .HasForeignKey(e => e.productManufacturer);

            modelBuilder.Entity<Product>()
                .Property(e => e.productCost)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Provider>()
                .HasMany(e => e.Product)
                .WithOptional(e => e.Provider)
                .HasForeignKey(e => e.productProvider);
        }
    }
}
