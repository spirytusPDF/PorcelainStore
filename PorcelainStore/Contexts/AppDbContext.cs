using Microsoft.EntityFrameworkCore;
using PorcelainStore.Models;

namespace PorcelainStore.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doll>();
            modelBuilder.Entity<Figurine>();
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        
    }
}
