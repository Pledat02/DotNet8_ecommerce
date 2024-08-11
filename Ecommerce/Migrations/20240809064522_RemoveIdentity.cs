using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class RemoveIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Staffs_id_staff",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_id_staff",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "id_staff",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "Staffid_staff",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Staffid_staff",
                table: "Orders",
                column: "Staffid_staff");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Staffs_Staffid_staff",
                table: "Orders",
                column: "Staffid_staff",
                principalTable: "Staffs",
                principalColumn: "id_staff");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Staffs_Staffid_staff",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_Staffid_staff",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Staffid_staff",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "id_staff",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_id_staff",
                table: "Orders",
                column: "id_staff");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Staffs_id_staff",
                table: "Orders",
                column: "id_staff",
                principalTable: "Staffs",
                principalColumn: "id_staff",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
