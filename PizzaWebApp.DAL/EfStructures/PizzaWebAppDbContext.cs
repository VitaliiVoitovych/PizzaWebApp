using Microsoft.EntityFrameworkCore;
using PizzaWebApp.Models.Entities;

namespace PizzaWebApp.DAL.EfStructures
{
    public class PizzaWebAppDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Pizza> Pizzas { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<Payment> Payments { get; set; } = null!;

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
