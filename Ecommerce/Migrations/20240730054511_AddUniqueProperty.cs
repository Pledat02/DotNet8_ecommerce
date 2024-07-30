using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_username",
                table: "Users",
                column: "username"
                );

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_name_company",
                table: "Suppliers",
                column: "name_company"
               );

            migrationBuilder.CreateIndex(
                name: "IX_Categories_name",
                table: "Categories",
                column: "name"
                );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_username",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Suppliers_name_company",
                table: "Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_Categories_name",
                table: "Categories");
        }
    }
}
