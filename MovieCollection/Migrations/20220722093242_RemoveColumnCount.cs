using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieCollection.Migrations
{
    public partial class RemoveColumnCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MovieCount",
                schema: "dbo",
                table: "Directors");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MovieCount",
                schema: "dbo",
                table: "Directors",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
