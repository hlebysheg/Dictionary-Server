using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WordBook.Migrations
{
    public partial class ManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Letter_Tests_TestId",
                table: "Letter");

            migrationBuilder.DropIndex(
                name: "IX_Letter_TestId",
                table: "Letter");

            migrationBuilder.DropColumn(
                name: "TestId",
                table: "Letter");

            migrationBuilder.CreateTable(
                name: "TestToLetters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestId = table.Column<int>(type: "int", nullable: false),
                    LetterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestToLetters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestToLetters_Letter_LetterId",
                        column: x => x.LetterId,
                        principalTable: "Letter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestToLetters_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestToLetters_LetterId",
                table: "TestToLetters",
                column: "LetterId");

            migrationBuilder.CreateIndex(
                name: "IX_TestToLetters_TestId",
                table: "TestToLetters",
                column: "TestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestToLetters");

            migrationBuilder.AddColumn<int>(
                name: "TestId",
                table: "Letter",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Letter_TestId",
                table: "Letter",
                column: "TestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Letter_Tests_TestId",
                table: "Letter",
                column: "TestId",
                principalTable: "Tests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
