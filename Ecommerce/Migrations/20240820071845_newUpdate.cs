using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class newUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Voucher_Users_Orders_id_order",
                table: "Voucher_Users");

            migrationBuilder.DropIndex(
                name: "IX_Voucher_Users_id_order",
                table: "Voucher_Users");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_id_voucher_user",
                table: "Orders",
                column: "id_voucher_user",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Voucher_Users_id_voucher_user",
                table: "Orders",
                column: "id_voucher_user",
                principalTable: "Voucher_Users",
                principalColumn: "id_voucher_User",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Voucher_Users_id_voucher_user",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_id_voucher_user",
                table: "Orders");

            migrationBuilder.CreateIndex(
                name: "IX_Voucher_Users_id_order",
                table: "Voucher_Users",
                column: "id_order",
                unique: true,
                filter: "[id_order] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Voucher_Users_Orders_id_order",
                table: "Voucher_Users",
                column: "id_order",
                principalTable: "Orders",
                principalColumn: "id_order",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
