using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WordBook.Migrations
{
    public partial class refresh2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_refreshTokens_Student_StudentId",
                table: "refreshTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_refreshTokens",
                table: "refreshTokens");

            migrationBuilder.RenameTable(
                name: "refreshTokens",
                newName: "RefreshTokens");

            migrationBuilder.RenameIndex(
                name: "IX_refreshTokens_StudentId",
                table: "RefreshTokens",
                newName: "IX_RefreshTokens_StudentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefreshTokens",
                table: "RefreshTokens",
                column: "JwtId");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_Student_StudentId",
                table: "RefreshTokens",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_Student_StudentId",
                table: "RefreshTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RefreshTokens",
                table: "RefreshTokens");

            migrationBuilder.RenameTable(
                name: "RefreshTokens",
                newName: "refreshTokens");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshTokens_StudentId",
                table: "refreshTokens",
                newName: "IX_refreshTokens_StudentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_refreshTokens",
                table: "refreshTokens",
                column: "JwtId");

            migrationBuilder.AddForeignKey(
                name: "FK_refreshTokens_Student_StudentId",
                table: "refreshTokens",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
