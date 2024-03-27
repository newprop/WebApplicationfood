using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class sppp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sqlCode = @"Create proc spDeletefood3
 @ID int
AS
delete from foodItems where foodID=@ID
delete from foods where ID=@ID;";
            migrationBuilder.Sql(sqlCode);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("drop proc spDeletefood3");
        }
    }
}
