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

        modelBuilder.Entity<Product>()
            .HasIndex(p => p.Name);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Orders)
            .WithOne(o => o.User)
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Order>()
            .HasMany(o => o.OrderItems)
            .WithOne(oi => oi.Order)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Product>().HasData(

            // ── FRESH BOUQUETS ───────────────────────────────────────────────
            new Product { Id = 1, Name = "Blush Peony Bouquet", Brand = "PetalCo", Category = "Fresh Bouquets",
                Description = "A dreamy arrangement of soft blush peonies, perfect for gifting or brightening any space.",
                Price = 48.99m, StockQuantity = 30,
                ImageUrl = "https://images.unsplash.com/photo-1587314168485-3236d6710814?auto=format&fit=crop&w=800&q=80" },

            new Product { Id = 2, Name = "Lavender Rose Bundle", Brand = "BloomHaus", Category = "Fresh Bouquets",
                Description = "Twelve long-stem lavender roses wrapped in soft kraft paper with a satin ribbon.",
                Price = 54.99m, StockQuantity = 25,
                ImageUrl = "https://images.unsplash.com/photo-1548094990-c16ca90f1f0d?auto=format&fit=crop&w=800&q=80" },

            new Product { Id = 3, Name = "Pastel Tulip Bunch", Brand = "PetalCo", Category = "Fresh Bouquets",
                Description = "A cheerful mix of pink, peach and cream tulips bundled in a cloud of baby's breath.",
                Price = 34.99m, StockQuantity = 40,
                ImageUrl = "https://images.unsplash.com/photo-1462275646964-a0e3386b89fa?auto=format&fit=crop&w=800&q=80" },

            new Product { Id = 4, Name = "White Lily Elegance", Brand = "BloomHaus", Category = "Fresh Bouquets",
                Description = "Pure white oriental lilies arranged with eucalyptus for a clean, minimal look.",
                Price = 62.99m, StockQuantity = 20,
                ImageUrl = "https://images.unsplash.com/photo-1496062031456-07b8f162a322?auto=format&fit=crop&w=800&q=80" },

            new Product { Id = 5, Name = "Garden Rose Medley", Brand = "FleurJoy", Category = "Fresh Bouquets",
                Description = "A lush mixed garden bouquet with roses, ranunculus, and sweet-smelling freesia.",
                Price = 58.99m, StockQuantity = 22,
                ImageUrl = "https://images.unsplash.com/photo-1455582916367-25f75bfc6710?auto=format&fit=crop&w=800&q=80" },

            new Product { Id = 6, Name = "Sunflower & Chamomile Bunch", Brand = "PetalCo", Category = "Fresh Bouquets",
                Description = "Bright sunflowers paired with delicate chamomile for a warm, cottagecore feel.",
                Price = 38.99m, StockQuantity = 35,
                ImageUrl = "https://images.unsplash.com/photo-1508610048659-a06b669e3321?auto=format&fit=crop&w=800&q=80" },

            new Product { Id = 7, Name = "Pink Carnation Cloud", Brand = "FleurJoy", Category = "Fresh Bouquets",
                Description = "Fluffy pink carnations gathered in a soft cloud-like bouquet, tied with a pastel bow.",
                Price = 29.99m, StockQuantity = 50,
                ImageUrl = "https://images.unsplash.com/photo-1582794543139-8ac9cb0f7b11?auto=format&fit=crop&w=800&q=80" },

            new Product { Id = 8, Name = "Baby Blue Hydrangea Bunch", Brand = "BloomHaus", Category = "Fresh Bouquets",
                Description = "Voluminous baby blue hydrangeas, each head full and lush - a statement piece for any room.",
                Price = 44.99m, StockQuantity = 28,
                ImageUrl = "https://images.unsplash.com/photo-1591886960571-74d43a9d4166?auto=format&fit=crop&w=800&q=80" },

            new Product { Id = 9, Name = "Peach Ranunculus Wrap", Brand = "PetalCo", Category = "Fresh Bouquets",
                Description = "Soft peach ranunculus wrapped in vellum paper with a gold twine tie.",
                Price = 42.99m, StockQuantity = 18,
                ImageUrl = "https://images.unsplash.com/photo-1444930694458-01babf71870c?auto=format&fit=crop&w=800&q=80" },

            new Product { Id = 10, Name = "Wildflower Meadow Posy", Brand = "FleurJoy", Category = "Fresh Bouquets",
                Description = "A hand-tied posy of seasonal wildflowers, fresh from the meadow to your doorstep.",
                Price = 27.99m, StockQuantity = 45,
                ImageUrl = "https://images.unsplash.com/photo-1468327768560-75b778cbb551?auto=format&fit=crop&w=800&q=80" },

            new Product { Id = 11, Name = "Cherry Blossom Spray", Brand = "BloomHaus", Category = "Fresh Bouquets",
                Description = "Delicate cherry blossom branches arranged with soft pink filler flowers.",
                Price = 52.99m, StockQuantity = 15,
                ImageUrl = "https://images.unsplash.com/photo-1522748906645-95d8adfd52c7?auto=format&fit=crop&w=800&q=80" },

            new Product { Id = 12, Name = "Mint & White Daisy Bundle", Brand = "PetalCo", Category = "Fresh Bouquets",
                Description = "Fresh white daisies with mint green leaves - clean, simple and so cheerful.",
                Price = 24.99m, StockQuantity = 60,
                ImageUrl = "https://images.unsplash.com/photo-1502977249166-824b3a8a4d6d?auto=format&fit=crop&w=800&q=80" },

            new Product { Id = 13, Name = "Sweet Pea Soft Bunch", Brand = "FleurJoy", Category = "Fresh Bouquets",
                Description = "Pastel sweet peas in blush, lavender and cream, lightly fragrant and airy.",
                Price = 36.99m, StockQuantity = 32,
                ImageUrl = "https://images.unsplash.com/photo-1519378058457-4c29a0a2efac?auto=format&fit=crop&w=800&q=80" },

            // ── DRIED & PRESERVED ────────────────────────────────────────────
            new Product { Id = 14, Name = "Dried Pampas Grass Bunch", Brand = "EverBloom", Category = "Dried & Preserved",
                Description = "Fluffy natural pampas grass stems - perfect for vases and boho home decor.",
                Price = 32.99m, StockQuantity = 55,
                ImageUrl = "https://images.unsplash.com/photo-1597848212624-a19eb35e2651?auto=format&fit=crop&w=800&q=80" },

            new Product { Id = 15, Name = "Preserved Rose Dome", Brand = "ForeverPetal", Category = "Dried & Preserved",
                Description = "A single preserved rose under a glass dome - lasts up to 3 years without water.",
                Price = 69.99m, StockQuantity = 20,
                ImageUrl = "https://images.unsplash.com/photo-1518895949257-7621c3c786d7?auto=format&fit=crop&w=800&q=80" },

            new Product { Id = 16, Name = "Dried Lavender Bundle", Brand = "EverBloom", Category = "Dried & Preserved",
                Description = "Fragrant dried lavender stems tied with twine - aromatherapy meets home decor.",
                Price = 18.99m, StockQuantity = 80,
                ImageUrl = "https://images.unsplash.com/photo-1611909023032-2d6b3134ecba?auto=format&fit=crop&w=800&q=80" },

            new Product { Id = 17, Name = "Blush Dried Peony Wreath", Brand = "ForeverPetal", Category = "Dried & Preserved",
                Description = "A handcrafted wreath of dried blush peonies and eucalyptus for your front door.",
                Price = 84.99m, StockQuantity = 12,
                ImageUrl = "https://images.unsplash.com/photo-1518895949257-7621c3c786d7?auto=format&fit=crop&w=800&q=80" },

            new Product { Id = 18, Name = "Dried Strawflower Bouquet", Brand = "EverBloom", Category = "Dried & Preserved",
                Description = "Bright strawflowers in pink, peach and cream - dried to perfection and long-lasting.",
                Price = 26.99m, StockQuantity = 40,
                ImageUrl = "https://images.unsplash.com/photo-1462275646964-a0e3386b89fa?auto=format&fit=crop&w=800&q=80" },

            new Product { Id = 19, Name = "Preserved Baby's Breath Jar", Brand = "ForeverPetal", Category = "Dried & Preserved",
                Description = "Delicate preserved baby's breath in a minimal glass jar - effortlessly cute.",
                Price = 22.99m, StockQuantity = 65,
                ImageUrl = "https://images.unsplash.com/photo-1508610048659-a06b669e3321?auto=format&fit=crop&w=800&q=80" },

            new Product { Id = 20, Name = "Dried Cotton Stem Bundle", Brand = "EverBloom", Category = "Dried & Preserved",
                Description = "Soft dried cotton stems for minimalist floral arrangements or boho styling.",
                Price = 21.99m, StockQuantity = 50,
                ImageUrl = "https://images.unsplash.com/photo-1444930694458-01babf71870c?auto=format&fit=crop&w=800&q=80" },

            new Product { Id = 21, Name = "Pink Preserved Hydrangea Box", Brand = "ForeverPetal", Category = "Dried & Preserved",
                Description = "Preserved pink hydrangeas in a keepsake gift box - ready to display forever.",
                Price = 54.99m, StockQuantity = 18,
                ImageUrl = "https://images.unsplash.com/photo-1591886960571-74d43a9d4166?auto=format&fit=crop&w=800&q=80" },

            new Product { Id = 22, Name = "Dried Bunny Tail Grass", Brand = "EverBloom", Category = "Dried & Preserved",
                Description = "Fluffy bunny tail grass stems - impossibly soft and adorable in any arrangement.",
                Price = 16.99m, StockQuantity = 90,
                ImageUrl = "https://images.unsplash.com/photo-1455582916367-25f75bfc6710?auto=format&fit=crop&w=800&q=80" },

            new Product { Id = 23, Name = "Vintage Rose Pressed Frame", Brand = "ForeverPetal", Category = "Dried & Preserved",
                Description = "Real pressed roses and wildflowers in a gold-frame - a piece of living art.",
                Price = 78.99m, StockQuantity = 10,
                ImageUrl = "https://images.unsplash.com/photo-1525310072745-f49212b5ac6d?auto=format&fit=crop&w=800&q=80" },

            new Product { Id = 24, Name = "Dried Lunaria Moon Stems", Brand = "EverBloom", Category = "Dried & Preserved",
                Description = "Silvery lunaria (honesty plant) stems that catch the light beautifully.",
                Price = 19.99m, StockQuantity = 45,
                ImageUrl = "https://images.unsplash.com/photo-1471086569966-db3eebc25a59?auto=format&fit=crop&w=800&q=80" },

            new Product { Id = 25, Name = "Blush & Cream Dried Bouquet", Brand = "ForeverPetal", Category = "Dried & Preserved",
                Description = "A curated mix of dried blush and cream blooms - romantic, timeless and zero maintenance.",
                Price = 44.99m, StockQuantity = 28,
                ImageUrl = "https://images.unsplash.com/photo-1561181286-d3fee7d55364?auto=format&fit=crop&w=800&q=80" }
        );
    }
}