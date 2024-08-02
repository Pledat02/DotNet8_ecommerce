using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class updateSetNullFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Voucher_Users_id_voucher_id_user",
                table: "Orders");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Voucher_Users_id_voucher_id_user",
                table: "Orders",
                columns: new[] { "id_voucher", "id_user" },
                principalTable: "Voucher_Users",
                principalColumns: new[] { "id_voucher", "id_user" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Voucher_Users_id_voucher_id_user",
                table: "Orders");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Voucher_Users_id_voucher_id_user",
                table: "Orders",
                columns: new[] { "id_voucher", "id_user" },
                principalTable: "Voucher_Users",
                principalColumns: new[] { "id_voucher", "id_user" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
