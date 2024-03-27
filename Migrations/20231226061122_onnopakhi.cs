using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class onnopakhi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "foods",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    foodName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    foodDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    foodCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_foods", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "foodItems",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    foodID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_foodItems", x => x.ItemId);
                    table.ForeignKey(
                        name: "FK_foodItems_foods_foodID",
                        column: x => x.foodID,
                        principalTable: "foods",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_foodItems_foodID",
                table: "foodItems",
                column: "foodID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "foodItems");

            migrationBuilder.DropTable(
                name: "foods");
        }
    }
}
