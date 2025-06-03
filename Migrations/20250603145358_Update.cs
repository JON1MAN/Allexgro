using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Allexgro.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttributeKeys_ProductAttributes_ProductAttributeId",
                table: "ProductAttributeKeys");

            migrationBuilder.DropIndex(
                name: "IX_ProductAttributeKeys_ProductAttributeId",
                table: "ProductAttributeKeys");

            migrationBuilder.DropColumn(
                name: "ProductAttributeId",
                table: "ProductAttributeKeys");

            migrationBuilder.AddColumn<int>(
                name: "AttributeKeyId",
                table: "ProductAttributes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string[]>(
                name: "AllowedValues",
                table: "ProductAttributeKeys",
                type: "text[]",
                nullable: false,
                defaultValue: new string[0]);

            migrationBuilder.AddColumn<bool>(
                name: "IsPredefined",
                table: "ProductAttributeKeys",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributes_AttributeKeyId",
                table: "ProductAttributes",
                column: "AttributeKeyId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttributes_ProductAttributeKeys_AttributeKeyId",
                table: "ProductAttributes",
                column: "AttributeKeyId",
                principalTable: "ProductAttributeKeys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttributes_ProductAttributeKeys_AttributeKeyId",
                table: "ProductAttributes");

            migrationBuilder.DropIndex(
                name: "IX_ProductAttributes_AttributeKeyId",
                table: "ProductAttributes");

            migrationBuilder.DropColumn(
                name: "AttributeKeyId",
                table: "ProductAttributes");

            migrationBuilder.DropColumn(
                name: "AllowedValues",
                table: "ProductAttributeKeys");

            migrationBuilder.DropColumn(
                name: "IsPredefined",
                table: "ProductAttributeKeys");

            migrationBuilder.AddColumn<int>(
                name: "ProductAttributeId",
                table: "ProductAttributeKeys",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeKeys_ProductAttributeId",
                table: "ProductAttributeKeys",
                column: "ProductAttributeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttributeKeys_ProductAttributes_ProductAttributeId",
                table: "ProductAttributeKeys",
                column: "ProductAttributeId",
                principalTable: "ProductAttributes",
                principalColumn: "Id");
        }
    }
}
