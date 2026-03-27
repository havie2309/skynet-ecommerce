using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Skinet.Api.Migrations
{
    /// <inheritdoc />
    public partial class ExpandFlowerCatalog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Brand", "Category", "Description", "ImageUrl", "Name", "Price", "StockQuantity" },
                values: new object[] { "PetalCo", "Fresh Bouquets", "A dreamy arrangement of soft blush peonies, perfect for gifting or brightening any space.", "https://images.unsplash.com/photo-1587314168485-3236d6710814?auto=format&fit=crop&w=800&q=80", "Blush Peony Bouquet", 48.99m, 30 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Brand", "Category", "Description", "ImageUrl", "Name", "Price", "StockQuantity" },
                values: new object[] { "BloomHaus", "Fresh Bouquets", "Twelve long-stem lavender roses wrapped in soft kraft paper with a satin ribbon.", "https://images.unsplash.com/photo-1548094990-c16ca90f1f0d?auto=format&fit=crop&w=800&q=80", "Lavender Rose Bundle", 54.99m, 25 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Brand", "Category", "Description", "ImageUrl", "Name", "Price", "StockQuantity" },
                values: new object[,]
                {
                    { 3, "PetalCo", "Fresh Bouquets", "A cheerful mix of pink, peach and cream tulips bundled in a cloud of baby's breath.", "https://images.unsplash.com/photo-1593957261936-31d7bcada2a9?auto=format&fit=crop&w=800&q=80", "Pastel Tulip Bunch", 34.99m, 40 },
                    { 4, "BloomHaus", "Fresh Bouquets", "Pure white oriental lilies arranged with eucalyptus for a clean, minimal look.", "https://images.unsplash.com/photo-1560717845-968823efbee1?auto=format&fit=crop&w=800&q=80", "White Lily Elegance", 62.99m, 20 },
                    { 5, "FleurJoy", "Fresh Bouquets", "A lush mixed garden bouquet with roses, ranunculus, and sweet-smelling freesia.", "https://images.unsplash.com/photo-1599598425947-5202edd56fdb?auto=format&fit=crop&w=800&q=80", "Garden Rose Medley", 58.99m, 22 },
                    { 6, "PetalCo", "Fresh Bouquets", "Bright sunflowers paired with delicate chamomile for a warm, cottagecore feel.", "https://images.unsplash.com/photo-1561181286-d3fee7d55364?auto=format&fit=crop&w=800&q=80", "Sunflower & Chamomile Bunch", 38.99m, 35 },
                    { 7, "FleurJoy", "Fresh Bouquets", "Fluffy pink carnations gathered in a soft cloud-like bouquet, tied with a pastel bow.", "https://images.unsplash.com/photo-1582794543139-8ac9cb0f7b11?auto=format&fit=crop&w=800&q=80", "Pink Carnation Cloud", 29.99m, 50 },
                    { 8, "BloomHaus", "Fresh Bouquets", "Voluminous baby blue hydrangeas, each head full and lush — a statement piece for any room.", "https://images.unsplash.com/photo-1591886960571-74d43a9d4166?auto=format&fit=crop&w=800&q=80", "Baby Blue Hydrangea Bunch", 44.99m, 28 },
                    { 9, "PetalCo", "Fresh Bouquets", "Soft peach ranunculus wrapped in vellum paper with a gold twine tie.", "https://images.unsplash.com/photo-1525310072745-f49212b5ac6d?auto=format&fit=crop&w=800&q=80", "Peach Ranunculus Wrap", 42.99m, 18 },
                    { 10, "FleurJoy", "Fresh Bouquets", "A hand-tied posy of seasonal wildflowers, fresh from the meadow to your doorstep.", "https://images.unsplash.com/photo-1490750967868-88df5691cc08?auto=format&fit=crop&w=800&q=80", "Wildflower Meadow Posy", 27.99m, 45 },
                    { 11, "BloomHaus", "Fresh Bouquets", "Delicate cherry blossom branches arranged with soft pink filler flowers.", "https://images.unsplash.com/photo-1522748906645-95d8adfd52c7?auto=format&fit=crop&w=800&q=80", "Cherry Blossom Spray", 52.99m, 15 },
                    { 12, "PetalCo", "Fresh Bouquets", "Fresh white daisies with mint green leaves — clean, simple and so cheerful.", "https://images.unsplash.com/photo-1502977249166-824b3a8a4d6d?auto=format&fit=crop&w=800&q=80", "Mint & White Daisy Bundle", 24.99m, 60 },
                    { 13, "FleurJoy", "Fresh Bouquets", "Pastel sweet peas in blush, lavender and cream, lightly fragrant and airy.", "https://images.unsplash.com/photo-1519378058457-4c29a0a2efac?auto=format&fit=crop&w=800&q=80", "Sweet Pea Soft Bunch", 36.99m, 32 },
                    { 14, "EverBloom", "Dried & Preserved", "Fluffy natural pampas grass stems — perfect for vases and boho home decor.", "https://images.unsplash.com/photo-1598300042247-d088f8ab3a91?auto=format&fit=crop&w=800&q=80", "Dried Pampas Grass Bunch", 32.99m, 55 },
                    { 15, "ForeverPetal", "Dried & Preserved", "A single preserved rose under a glass dome — lasts up to 3 years without water.", "https://images.unsplash.com/photo-1519378058457-4c29a0a2efac?auto=format&fit=crop&w=800&q=80", "Preserved Rose Dome", 69.99m, 20 },
                    { 16, "EverBloom", "Dried & Preserved", "Fragrant dried lavender stems tied with twine — aromatherapy meets home decor.", "https://images.unsplash.com/photo-1499578124509-1611b77778e8?auto=format&fit=crop&w=800&q=80", "Dried Lavender Bundle", 18.99m, 80 },
                    { 17, "ForeverPetal", "Dried & Preserved", "A handcrafted wreath of dried blush peonies and eucalyptus for your front door.", "https://images.unsplash.com/photo-1542838775-bda000e36f93?auto=format&fit=crop&w=800&q=80", "Blush Dried Peony Wreath", 84.99m, 12 },
                    { 18, "EverBloom", "Dried & Preserved", "Bright strawflowers in pink, peach and cream — dried to perfection and long-lasting.", "https://images.unsplash.com/photo-1455659817273-f96807779a8a?auto=format&fit=crop&w=800&q=80", "Dried Strawflower Bouquet", 26.99m, 40 },
                    { 19, "ForeverPetal", "Dried & Preserved", "Delicate preserved baby's breath in a minimal glass jar — effortlessly cute.", "https://images.unsplash.com/photo-1508610048659-a06b669e3321?auto=format&fit=crop&w=800&q=80", "Preserved Baby's Breath Jar", 22.99m, 65 },
                    { 20, "EverBloom", "Dried & Preserved", "Soft dried cotton stems for minimalist floral arrangements or boho styling.", "https://images.unsplash.com/photo-1543258103-a62bdc069871?auto=format&fit=crop&w=800&q=80", "Dried Cotton Stem Bundle", 21.99m, 50 },
                    { 21, "ForeverPetal", "Dried & Preserved", "Preserved pink hydrangeas in a keepsake gift box — ready to display forever.", "https://images.unsplash.com/photo-1519378058457-4c29a0a2efac?auto=format&fit=crop&w=800&q=80", "Pink Preserved Hydrangea Box", 54.99m, 18 },
                    { 22, "EverBloom", "Dried & Preserved", "Fluffy bunny tail grass stems — impossibly soft and adorable in any arrangement.", "https://images.unsplash.com/photo-1471086569966-db3eebc25a59?auto=format&fit=crop&w=800&q=80", "Dried Bunny Tail Grass", 16.99m, 90 },
                    { 23, "ForeverPetal", "Dried & Preserved", "Real pressed roses and wildflowers in a gold-frame — a piece of living art.", "https://images.unsplash.com/photo-1490750967868-88df5691cc08?auto=format&fit=crop&w=800&q=80", "Vintage Rose Pressed Frame", 78.99m, 10 },
                    { 24, "EverBloom", "Dried & Preserved", "Silvery lunaria (honesty plant) stems that catch the light beautifully.", "https://images.unsplash.com/photo-1508193638397-1c4234db14d8?auto=format&fit=crop&w=800&q=80", "Dried Lunaria Moon Stems", 19.99m, 45 },
                    { 25, "ForeverPetal", "Dried & Preserved", "A curated mix of dried blush and cream blooms — romantic, timeless and zero maintenance.", "https://images.unsplash.com/photo-1561181286-d3fee7d55364?auto=format&fit=crop&w=800&q=80", "Blush & Cream Dried Bouquet", 44.99m, 28 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Brand", "Category", "Description", "ImageUrl", "Name", "Price", "StockQuantity" },
                values: new object[] { "", "", "Gaming laptop", "https://images.unsplash.com/photo-1496181133206-80ce9b88a853?auto=format&fit=crop&w=800&q=80", "Laptop Pro", 1200m, 10 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Brand", "Category", "Description", "ImageUrl", "Name", "Price", "StockQuantity" },
                values: new object[] { "", "", "Wireless mouse", "https://images.unsplash.com/photo-1527814050087-3793815479db?auto=format&fit=crop&w=800&q=80", "Mouse", 50m, 100 });
        }
    }
}
