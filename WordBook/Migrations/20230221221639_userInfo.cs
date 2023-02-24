using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WordBook.Migrations
{
    public partial class userInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dictionary_StudentLogin_AuthorId",
                table: "Dictionary");

            migrationBuilder.AddForeignKey(
                name: "FK_Dictionary_StudentInfo_AuthorId",
                table: "Dictionary",
                column: "AuthorId",
                principalTable: "StudentInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dictionary_StudentInfo_AuthorId",
                table: "Dictionary");

            migrationBuilder.AddForeignKey(
                name: "FK_Dictionary_StudentLogin_AuthorId",
                table: "Dictionary",
                column: "AuthorId",
                principalTable: "StudentLogin",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
