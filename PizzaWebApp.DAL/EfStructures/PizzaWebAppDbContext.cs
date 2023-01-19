using Microsoft.EntityFrameworkCore;
using PizzaWebApp.Models.Entities;

namespace PizzaWebApp.DAL.EfStructures
{
    public class PizzaWebAppDbContext : DbContext
    {
        public DbSet<Person> People { get; set; } = null!;
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
            modelBuilder.Entity<Person>(entity =>
            {
                entity.Property(c => c.FullName)
                      .HasColumnName(nameof(Person.FullName))
                      .HasComputedColumnSql("[LastName] + ' ' + [FirstName]");
            });
        }
    }
}
