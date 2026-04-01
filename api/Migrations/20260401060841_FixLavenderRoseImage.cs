using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Skinet.Api.Migrations
{
    /// <inheritdoc />
    public partial class FixLavenderRoseImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1468327768560-75b778cbb551?auto=format&fit=crop&w=800&q=80");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1548094990-c16ca90f1f0d?auto=format&fit=crop&w=800&q=80");
        }
    }
}
