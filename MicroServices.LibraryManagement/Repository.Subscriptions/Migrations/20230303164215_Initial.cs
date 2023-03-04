using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Subscriptions.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SUBSCRIBER_NAME = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DATE_SUBSCRIBED = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DATE_RETURNED = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BOOK_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Subscriptions",
                columns: new[] { "Id", "BOOK_ID", "DATE_RETURNED", "DATE_SUBSCRIBED", "SUBSCRIBER_NAME" },
                values: new object[] { 1, 1, null, new DateTime(2023, 3, 3, 16, 42, 15, 277, DateTimeKind.Utc).AddTicks(3610), "Paritosh" });

            migrationBuilder.InsertData(
                table: "Subscriptions",
                columns: new[] { "Id", "BOOK_ID", "DATE_RETURNED", "DATE_SUBSCRIBED", "SUBSCRIBER_NAME" },
                values: new object[] { 2, 2, null, new DateTime(2023, 3, 3, 16, 42, 15, 277, DateTimeKind.Utc).AddTicks(3615), "SecondUser" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subscriptions");
        }
    }
}
