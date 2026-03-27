using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Skinet.Api.Migrations
{
    /// <inheritdoc />
    public partial class FixSevenImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1508610048659-a06b669e3321?auto=format&fit=crop&w=800&q=80");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                column: "Description",
                value: "Voluminous baby blue hydrangeas, each head full and lush - a statement piece for any room.");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                column: "Description",
                value: "Fresh white daisies with mint green leaves - clean, simple and so cheerful.");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13,
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1519378058457-4c29a0a2efac?auto=format&fit=crop&w=800&q=80");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14,
                column: "Description",
                value: "Fluffy natural pampas grass stems - perfect for vases and boho home decor.");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15,
                column: "Description",
                value: "A single preserved rose under a glass dome - lasts up to 3 years without water.");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "Description", "ImageUrl" },
                values: new object[] { "Fragrant dried lavender stems tied with twine - aromatherapy meets home decor.", "https://images.unsplash.com/photo-1611909023032-2d6b3134ecba?auto=format&fit=crop&w=800&q=80" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17,
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1599598425947-5202edd56fdb?auto=format&fit=crop&w=800&q=80");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18,
                column: "Description",
                value: "Bright strawflowers in pink, peach and cream - dried to perfection and long-lasting.");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 19,
                column: "Description",
                value: "Delicate preserved baby's breath in a minimal glass jar - effortlessly cute.");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 20,
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1444930694458-01babf71870c?auto=format&fit=crop&w=800&q=80");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "Description", "ImageUrl" },
                values: new object[] { "Preserved pink hydrangeas in a keepsake gift box - ready to display forever.", "https://images.unsplash.com/photo-1497366216548-37526070297c?auto=format&fit=crop&w=800&q=80" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Description", "ImageUrl" },
                values: new object[] { "Fluffy bunny tail grass stems - impossibly soft and adorable in any arrangement.", "https://images.unsplash.com/photo-1455582916367-25f75bfc6710?auto=format&fit=crop&w=800&q=80" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 23,
                column: "Description",
                value: "Real pressed roses and wildflowers in a gold-frame - a piece of living art.");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 25,
                column: "Description",
                value: "A curated mix of dried blush and cream blooms - romantic, timeless and zero maintenance.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1470509037663-253d462dc0f9?auto=format&fit=crop&w=800&q=80");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                column: "Description",
                value: "Voluminous baby blue hydrangeas, each head full and lush — a statement piece for any room.");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                column: "Description",
                value: "Fresh white daisies with mint green leaves — clean, simple and so cheerful.");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13,
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1487530811015-780bfb1fef1a?auto=format&fit=crop&w=800&q=80");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14,
                column: "Description",
                value: "Fluffy natural pampas grass stems — perfect for vases and boho home decor.");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15,
                column: "Description",
                value: "A single preserved rose under a glass dome — lasts up to 3 years without water.");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "Description", "ImageUrl" },
                values: new object[] { "Fragrant dried lavender stems tied with twine — aromatherapy meets home decor.", "https://images.unsplash.com/photo-1499578124509-1611b77778e8?auto=format&fit=crop&w=800&q=80" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17,
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1559181567-c3190900f8b5?auto=format&fit=crop&w=800&q=80");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18,
                column: "Description",
                value: "Bright strawflowers in pink, peach and cream — dried to perfection and long-lasting.");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 19,
                column: "Description",
                value: "Delicate preserved baby's breath in a minimal glass jar — effortlessly cute.");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 20,
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1490750967868-88df5691cc08?auto=format&fit=crop&w=800&q=80");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "Description", "ImageUrl" },
                values: new object[] { "Preserved pink hydrangeas in a keepsake gift box — ready to display forever.", "https://images.unsplash.com/photo-1560717845-968823efbee1?auto=format&fit=crop&w=800&q=80" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Description", "ImageUrl" },
                values: new object[] { "Fluffy bunny tail grass stems — impossibly soft and adorable in any arrangement.", "https://images.unsplash.com/photo-1598300042247-d088f8ab3a91?auto=format&fit=crop&w=800&q=80" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 23,
                column: "Description",
                value: "Real pressed roses and wildflowers in a gold-frame — a piece of living art.");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 25,
                column: "Description",
                value: "A curated mix of dried blush and cream blooms — romantic, timeless and zero maintenance.");
        }
    }
}
