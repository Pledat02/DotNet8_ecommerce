using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePropertyOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Orders_id_order",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Voucher_Users_id_voucher_id_user",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Voucher_Users_Users_id_user",
                table: "Voucher_Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Voucher_Users_Vouchers_id_voucher",
                table: "Voucher_Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Voucher_Users",
                table: "Voucher_Users");

            migrationBuilder.DropIndex(
                name: "IX_Orders_id_voucher_id_user",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "id_user",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "id_voucher",
                table: "Orders",
                newName: "id_voucher_user");

            migrationBuilder.AlterColumn<int>(
                name: "id_user",
                table: "Voucher_Users",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "id_voucher",
                table: "Voucher_Users",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "id_voucher_User",
                table: "Voucher_Users",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Voucher_Users",
                table: "Voucher_Users",
                column: "id_voucher_User");

            migrationBuilder.CreateIndex(
                name: "IX_Voucher_Users_id_order",
                table: "Voucher_Users",
                column: "id_order",
                unique: true,
                filter: "[id_order] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Voucher_Users_id_voucher",
                table: "Voucher_Users",
                column: "id_voucher");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Orders_id_order",
                table: "Bills",
                column: "id_order",
                principalTable: "Orders",
                principalColumn: "id_order",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Voucher_Users_Orders_id_order",
                table: "Voucher_Users",
                column: "id_order",
                principalTable: "Orders",
                principalColumn: "id_order",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Voucher_Users_Users_id_user",
                table: "Voucher_Users",
                column: "id_user",
                principalTable: "Users",
                principalColumn: "id_user",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Voucher_Users_Vouchers_id_voucher",
                table: "Voucher_Users",
                column: "id_voucher",
                principalTable: "Vouchers",
                principalColumn: "id_voucher",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Orders_id_order",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Voucher_Users_Orders_id_order",
                table: "Voucher_Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Voucher_Users_Users_id_user",
                table: "Voucher_Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Voucher_Users_Vouchers_id_voucher",
                table: "Voucher_Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Voucher_Users",
                table: "Voucher_Users");

            migrationBuilder.DropIndex(
                name: "IX_Voucher_Users_id_order",
                table: "Voucher_Users");

            migrationBuilder.DropIndex(
                name: "IX_Voucher_Users_id_voucher",
                table: "Voucher_Users");

            migrationBuilder.DropColumn(
                name: "id_voucher_User",
                table: "Voucher_Users");

            migrationBuilder.RenameColumn(
                name: "id_voucher_user",
                table: "Orders",
                newName: "id_voucher");

            migrationBuilder.AlterColumn<int>(
                name: "id_voucher",
                table: "Voucher_Users",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "id_user",
                table: "Voucher_Users",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "id_user",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Voucher_Users",
                table: "Voucher_Users",
                columns: new[] { "id_voucher", "id_user" });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_id_voucher_id_user",
                table: "Orders",
                columns: new[] { "id_voucher", "id_user" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Orders_id_order",
                table: "Bills",
                column: "id_order",
                principalTable: "Orders",
                principalColumn: "id_order",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Voucher_Users_id_voucher_id_user",
                table: "Orders",
                columns: new[] { "id_voucher", "id_user" },
                principalTable: "Voucher_Users",
                principalColumns: new[] { "id_voucher", "id_user" },
                onDelete: ReferentialAction.Cascade);

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
    }
}
