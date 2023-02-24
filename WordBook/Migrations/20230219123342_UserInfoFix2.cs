using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WordBook.Migrations
{
    public partial class UserInfoFix2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestToStudents_StudentLogin_StudentId",
                table: "TestToStudents");

            migrationBuilder.DropIndex(
                name: "IX_TestToStudents_StudentId",
                table: "TestToStudents");

            migrationBuilder.AddColumn<int>(
                name: "StudentLoginId",
                table: "TestToStudents",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TestToStudents_StudentLoginId",
                table: "TestToStudents",
                column: "StudentLoginId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestToStudents_StudentLogin_StudentLoginId",
                table: "TestToStudents",
                column: "StudentLoginId",
                principalTable: "StudentLogin",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestToStudents_StudentLogin_StudentLoginId",
                table: "TestToStudents");

            migrationBuilder.DropIndex(
                name: "IX_TestToStudents_StudentLoginId",
                table: "TestToStudents");

            migrationBuilder.DropColumn(
                name: "StudentLoginId",
                table: "TestToStudents");

            migrationBuilder.CreateIndex(
                name: "IX_TestToStudents_StudentId",
                table: "TestToStudents",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestToStudents_StudentLogin_StudentId",
                table: "TestToStudents",
                column: "StudentId",
                principalTable: "StudentLogin",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
