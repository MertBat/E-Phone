using E_Phone.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Phone.DAL.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
            
        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
       
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Core.Entities.Version> Versions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>().Property(b=> b.BrandName).HasMaxLength(50);

            modelBuilder.Entity<Model>().Property(m => m.ModelName).HasMaxLength(50);

            modelBuilder.Entity<Order>().Property(o => o.TotalPrice).IsRequired();
            modelBuilder.Entity<Order>().Property(o => o.OrderCount).IsRequired();

            modelBuilder.Entity<Core.Entities.Version>().Property(p=> p.StorageCapacity).IsRequired();
            modelBuilder.Entity<Core.Entities.Version>().Property(p => p.price).IsRequired();
            modelBuilder.Entity<Core.Entities.Version>().Property(p => p.Stock).IsRequired();

            modelBuilder.Entity<User>().Property(u => u.Name).HasMaxLength(50);
            modelBuilder.Entity<User>().Property(u => u.Surname).HasMaxLength(50);

            base.OnModelCreating(modelBuilder);
        }

    }
}
