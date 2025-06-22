using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Allexgro.Migrations
{
    /// <inheritdoc />
    public partial class _22_06_2025_13_10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAccountLinkExpiration",
                table: "StripeUserAccountDetails",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdateAccountLinkUrl",
                table: "StripeUserAccountDetails",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdateAccountLinkExpiration",
                table: "StripeUserAccountDetails");

            migrationBuilder.DropColumn(
                name: "UpdateAccountLinkUrl",
                table: "StripeUserAccountDetails");
        }
    }
}
