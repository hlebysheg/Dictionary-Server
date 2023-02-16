using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WordBook.Migrations
{
    public partial class ClearTestToLettr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestToLetters");

            migrationBuilder.AddColumn<int>(
                name: "LetterId",
                table: "Answers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TestId",
                table: "Answers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Answers_LetterId",
                table: "Answers",
                column: "LetterId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_TestId",
                table: "Answers",
                column: "TestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Letter_LetterId",
                table: "Answers",
                column: "LetterId",
                principalTable: "Letter",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Tests_TestId",
                table: "Answers",
                column: "TestId",
                principalTable: "Tests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Letter_LetterId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Tests_TestId",
                table: "Answers");

            migrationBuilder.DropIndex(
                name: "IX_Answers_LetterId",
                table: "Answers");

            migrationBuilder.DropIndex(
                name: "IX_Answers_TestId",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "LetterId",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "TestId",
                table: "Answers");

            migrationBuilder.CreateTable(
                name: "TestToLetters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnswerId = table.Column<int>(type: "int", nullable: false),
                    LetterId = table.Column<int>(type: "int", nullable: false),
                    TestId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestToLetters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestToLetters_Answers_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "Answers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_TestToLetters_AnswerId",
                table: "TestToLetters",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_TestToLetters_LetterId",
                table: "TestToLetters",
                column: "LetterId");

            migrationBuilder.CreateIndex(
                name: "IX_TestToLetters_TestId",
                table: "TestToLetters",
                column: "TestId");
        }
    }
}
