using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaseProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BorrowingDetails_Users_UserId",
                table: "BorrowingDetails");

            migrationBuilder.DropIndex(
                name: "IX_BorrowingDetails_UserId",
                table: "BorrowingDetails");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "BorrowingDetails");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Borrowings",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Borrowings_UserId",
                table: "Borrowings",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Borrowings_Users_UserId",
                table: "Borrowings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Borrowings_Users_UserId",
                table: "Borrowings");

            migrationBuilder.DropIndex(
                name: "IX_Borrowings_UserId",
                table: "Borrowings");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Borrowings");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "BorrowingDetails",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BorrowingDetails_UserId",
                table: "BorrowingDetails",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowingDetails_Users_UserId",
                table: "BorrowingDetails",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}