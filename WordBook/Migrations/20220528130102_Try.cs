using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WordBook.Migrations
{
    public partial class Try : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Letter_WordBook_WordBookId",
                table: "Letter");

            migrationBuilder.DropTable(
                name: "WordBook");

            migrationBuilder.RenameColumn(
                name: "WordBookId",
                table: "Letter",
                newName: "DictionaryId");

            migrationBuilder.RenameIndex(
                name: "IX_Letter_WordBookId",
                table: "Letter",
                newName: "IX_Letter_DictionaryId");

            migrationBuilder.CreateTable(
                name: "Dictionary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    language = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dictionary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dictionary_Student_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dictionary_AuthorId",
                table: "Dictionary",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Letter_Dictionary_DictionaryId",
                table: "Letter",
                column: "DictionaryId",
                principalTable: "Dictionary",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Letter_Dictionary_DictionaryId",
                table: "Letter");

            migrationBuilder.DropTable(
                name: "Dictionary");

            migrationBuilder.RenameColumn(
                name: "DictionaryId",
                table: "Letter",
                newName: "WordBookId");

            migrationBuilder.RenameIndex(
                name: "IX_Letter_DictionaryId",
                table: "Letter",
                newName: "IX_Letter_WordBookId");

            migrationBuilder.CreateTable(
                name: "WordBook",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    language = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordBook", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WordBook_Student_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WordBook_AuthorId",
                table: "WordBook",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Letter_WordBook_WordBookId",
                table: "Letter",
                column: "WordBookId",
                principalTable: "WordBook",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
