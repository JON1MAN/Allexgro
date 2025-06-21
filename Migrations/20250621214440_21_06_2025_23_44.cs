using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Allexgro.Migrations
{
    /// <inheritdoc />
    public partial class _21_06_2025_23_44 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StripeUserAccountDetails",
                columns: table => new
                {
                    userId = table.Column<string>(type: "text", nullable: false),
                    stripeAccountId = table.Column<string>(type: "text", nullable: false),
                    OnboardingAccountLinkUrl = table.Column<string>(type: "text", nullable: false),
                    OnboardingAccountLinkExpiration = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StripeUserAccountDetails", x => x.userId);
                    table.ForeignKey(
                        name: "FK_StripeUserAccountDetails_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StripeUserAccountDetails");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "AspNetUsers");
        }
    }
}
