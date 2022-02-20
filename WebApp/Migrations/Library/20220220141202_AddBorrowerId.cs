using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Migrations.Library
{
    public partial class AddBorrowerId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookInstances_User_UserId",
                table: "BookInstances");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "BookInstances",
                newName: "BorrowerId");

            migrationBuilder.RenameIndex(
                name: "IX_BookInstances_UserId",
                table: "BookInstances",
                newName: "IX_BookInstances_BorrowerId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookInstances_User_BorrowerId",
                table: "BookInstances",
                column: "BorrowerId",
                principalTable: "User",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookInstances_User_BorrowerId",
                table: "BookInstances");

            migrationBuilder.RenameColumn(
                name: "BorrowerId",
                table: "BookInstances",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_BookInstances_BorrowerId",
                table: "BookInstances",
                newName: "IX_BookInstances_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookInstances_User_UserId",
                table: "BookInstances",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
