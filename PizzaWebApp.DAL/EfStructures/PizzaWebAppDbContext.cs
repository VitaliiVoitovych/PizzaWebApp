using Microsoft.EntityFrameworkCore;
using PizzaWebApp.Models.Entities;

namespace PizzaWebApp.DAL.EfStructures
{
    public class PizzaWebAppDbContext : DbContext
    {
        public DbSet<Customer>? Customers { get; set; }
        public DbSet<Pizza>? Pizzas { get; set; }
        public DbSet<Order>? Orders { get; set; }
        public DbSet<Payment>? Payments { get; set; }

        public PizzaWebAppDbContext(DbContextOptions<PizzaWebAppDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(c => c.FullName)
                      .HasColumnName(nameof(Customer.FullName))
                      .HasComputedColumnSql("[LastName] + ' ' + [FirstName]");
            });
        }
    }
}
