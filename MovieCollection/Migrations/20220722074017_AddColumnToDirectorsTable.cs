using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieCollection.Migrations
{
    public partial class AddColumnToDirectorsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MovieCount",
                schema: "dbo",
                table: "Directors",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MovieCount",
                schema: "dbo",
                table: "Directors");
        }
    }
}
