using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Books.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BOOK_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BOOK_NAME = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    AUTHOR = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    AVAILABLE_COPIES = table.Column<int>(type: "int", nullable: false),
                    TOTAL_COPIES = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BOOK_ID);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BOOK_ID", "AUTHOR", "AVAILABLE_COPIES", "BOOK_NAME", "TOTAL_COPIES" },
                values: new object[,]
                {
                    { 1, "Dan Brown", 5, "Da Vinci Code", 5 },
                    { 2, "Dan Brown", 3, "The Deception Point", 3 },
                    { 3, "Agatha Christi", 3, "And then there were none", 3 },
                    { 4, "Abdul Kalam", 5, "Wings of Fire", 5 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
