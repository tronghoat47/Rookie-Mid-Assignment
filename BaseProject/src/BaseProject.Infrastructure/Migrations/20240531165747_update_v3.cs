using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaseProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BorrowingDetails_Books_BookId",
                table: "BorrowingDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_BorrowingDetails_Borrowings_BorrowingId",
                table: "BorrowingDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Borrowings_Users_ApproverId",
                table: "Borrowings");

            migrationBuilder.DropForeignKey(
                name: "FK_Borrowings_Users_RequestorId",
                table: "Borrowings");

            migrationBuilder.DropForeignKey(
                name: "FK_Borrowings_Users_UserId",
                table: "Borrowings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Borrowings",
                table: "Borrowings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BorrowingDetails",
                table: "BorrowingDetails");

            migrationBuilder.RenameTable(
                name: "Borrowings",
                newName: "BookBorrowingRequest");

            migrationBuilder.RenameTable(
                name: "BorrowingDetails",
                newName: "BookBorrowingRequestDetail");

            migrationBuilder.RenameIndex(
                name: "IX_Borrowings_UserId",
                table: "BookBorrowingRequest",
                newName: "IX_BookBorrowingRequest_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Borrowings_RequestorId",
                table: "BookBorrowingRequest",
                newName: "IX_BookBorrowingRequest_RequestorId");

            migrationBuilder.RenameIndex(
                name: "IX_Borrowings_ApproverId",
                table: "BookBorrowingRequest",
                newName: "IX_BookBorrowingRequest_ApproverId");

            migrationBuilder.RenameIndex(
                name: "IX_BorrowingDetails_BookId",
                table: "BookBorrowingRequestDetail",
                newName: "IX_BookBorrowingRequestDetail_BookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookBorrowingRequest",
                table: "BookBorrowingRequest",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookBorrowingRequestDetail",
                table: "BookBorrowingRequestDetail",
                columns: new[] { "BorrowingId", "BookId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BookBorrowingRequest_Users_ApproverId",
                table: "BookBorrowingRequest",
                column: "ApproverId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookBorrowingRequest_Users_RequestorId",
                table: "BookBorrowingRequest",
                column: "RequestorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookBorrowingRequest_Users_UserId",
                table: "BookBorrowingRequest",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookBorrowingRequestDetail_BookBorrowingRequest_BorrowingId",
                table: "BookBorrowingRequestDetail",
                column: "BorrowingId",
                principalTable: "BookBorrowingRequest",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookBorrowingRequestDetail_Books_BookId",
                table: "BookBorrowingRequestDetail",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookBorrowingRequest_Users_ApproverId",
                table: "BookBorrowingRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_BookBorrowingRequest_Users_RequestorId",
                table: "BookBorrowingRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_BookBorrowingRequest_Users_UserId",
                table: "BookBorrowingRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_BookBorrowingRequestDetail_BookBorrowingRequest_BorrowingId",
                table: "BookBorrowingRequestDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_BookBorrowingRequestDetail_Books_BookId",
                table: "BookBorrowingRequestDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookBorrowingRequestDetail",
                table: "BookBorrowingRequestDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookBorrowingRequest",
                table: "BookBorrowingRequest");

            migrationBuilder.RenameTable(
                name: "BookBorrowingRequestDetail",
                newName: "BorrowingDetails");

            migrationBuilder.RenameTable(
                name: "BookBorrowingRequest",
                newName: "Borrowings");

            migrationBuilder.RenameIndex(
                name: "IX_BookBorrowingRequestDetail_BookId",
                table: "BorrowingDetails",
                newName: "IX_BorrowingDetails_BookId");

            migrationBuilder.RenameIndex(
                name: "IX_BookBorrowingRequest_UserId",
                table: "Borrowings",
                newName: "IX_Borrowings_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_BookBorrowingRequest_RequestorId",
                table: "Borrowings",
                newName: "IX_Borrowings_RequestorId");

            migrationBuilder.RenameIndex(
                name: "IX_BookBorrowingRequest_ApproverId",
                table: "Borrowings",
                newName: "IX_Borrowings_ApproverId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BorrowingDetails",
                table: "BorrowingDetails",
                columns: new[] { "BorrowingId", "BookId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Borrowings",
                table: "Borrowings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowingDetails_Books_BookId",
                table: "BorrowingDetails",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowingDetails_Borrowings_BorrowingId",
                table: "BorrowingDetails",
                column: "BorrowingId",
                principalTable: "Borrowings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Borrowings_Users_ApproverId",
                table: "Borrowings",
                column: "ApproverId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Borrowings_Users_RequestorId",
                table: "Borrowings",
                column: "RequestorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Borrowings_Users_UserId",
                table: "Borrowings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}