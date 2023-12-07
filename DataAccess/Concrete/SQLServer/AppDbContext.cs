using Core.Configuration;
using Core.Entities.Concrete;
using Entities.Concrete;
using Entities.DTOs.UserDTOs;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.SQLServer
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(DatabaseConfiguration.ConnectionString);
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Specification> Specifications { get; set; }
        public DbSet<User> AppUser { get; set; }
        public DbSet<AppRole> AppRole { get; set; }
        public DbSet<AppUserRole> AppUserRoles { get; set; }
        public DbSet<WishList> WishLists { get; set; }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.Entity<UserOrderDTO>().ToView(null);
        //}
    }
}
