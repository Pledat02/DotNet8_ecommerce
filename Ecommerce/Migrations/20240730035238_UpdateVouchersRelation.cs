using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class UpdateVouchersRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Voucher_Users_Users_id_user",
                table: "Voucher_Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Voucher_Users_Vouchers_id_voucher",
                table: "Voucher_Users");

            migrationBuilder.AddForeignKey(
                name: "FK_Voucher_Users_Users_id_user",
                table: "Voucher_Users",
                column: "id_user",
                principalTable: "Users",
                principalColumn: "id_user",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Voucher_Users_Vouchers_id_voucher",
                table: "Voucher_Users",
                column: "id_voucher",
                principalTable: "Vouchers",
                principalColumn: "id_voucher",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Voucher_Users_Users_id_user",
                table: "Voucher_Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Voucher_Users_Vouchers_id_voucher",
                table: "Voucher_Users");

            migrationBuilder.AddForeignKey(
                name: "FK_Voucher_Users_Users_id_user",
                table: "Voucher_Users",
                column: "id_user",
                principalTable: "Users",
                principalColumn: "id_user",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Voucher_Users_Vouchers_id_voucher",
                table: "Voucher_Users",
                column: "id_voucher",
                principalTable: "Vouchers",
                principalColumn: "id_voucher",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
