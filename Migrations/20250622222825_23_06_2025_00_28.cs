using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Allexgro.Migrations
{
    /// <inheritdoc />
    public partial class _23_06_2025_00_28 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StripeProductId",
                table: "Products",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StripeProductPriceId",
                table: "Products",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StripeProductId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "StripeProductPriceId",
                table: "Products");
        }
    }
}
