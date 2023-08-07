using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using WebApplication2.Models;

namespace WebApplication2.Data
{
    public class StoreContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>().HasKey(b => b.Id);

            modelBuilder.Entity<Store>().HasKey(s => s.StoreNumber);

            modelBuilder.Entity<Laptop>().HasKey(l => l.Number);

            modelBuilder.Entity<LaptopStore>().HasKey(ls => ls.LaptopStoreId);

            modelBuilder.Entity<LaptopStore>()
                .HasOne(sl => sl.Laptop)
                .WithMany(l => l.laptopStores)
                .HasForeignKey(l => l.LaptopId);

            modelBuilder.Entity<LaptopStore>()
                .HasOne(sl => sl.Store)
                .WithMany(s => s.laptopStores)
                .HasForeignKey(s => s.StoreId);

        }

        public StoreContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Laptop> Laptops { get; set; } = null!;
        public DbSet<Brand> Brands { get; set; } = null!;
        public DbSet<Store> Stores { get; set; } = null!;
        public DbSet<LaptopStore> LaptopStores { get; set; } = null!;


    }
}
