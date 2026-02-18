using Microsoft.EntityFrameworkCore;
using Skinet.Api.Models;

namespace Skinet.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>().HasData(
            new Product
            {
                Id = 1,
                Name = "Laptop",
                Description = "Gaming laptop",
                Price = 1200m,
                StockQuantity = 10
            },
            new Product
            {
                Id = 2,
                Name = "Mouse",
                Description = "Wireless mouse",
                Price = 50m,
                StockQuantity = 100
            }
        );
    }
}
