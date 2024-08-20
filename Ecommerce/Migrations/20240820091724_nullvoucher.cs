using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class nullvoucher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Orders_id_voucher_user",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "id_voucher_user",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_id_voucher_user",
                table: "Orders",
                column: "id_voucher_user",
                unique: true,
                filter: "[id_voucher_user] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Orders_id_voucher_user",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "id_voucher_user",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_id_voucher_user",
                table: "Orders",
                column: "id_voucher_user",
                unique: true);
        }
    }
}
