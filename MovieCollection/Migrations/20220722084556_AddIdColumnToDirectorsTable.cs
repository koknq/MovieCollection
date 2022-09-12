using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieCollection.Migrations
{
    public partial class AddIdColumnToDirectorsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Directors_DirectorName",
                schema: "dbo",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_DirectorName",
                schema: "dbo",
                table: "Movies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Directors",
                schema: "dbo",
                table: "Directors");

            migrationBuilder.DropColumn(
                name: "DirectorName",
                schema: "dbo",
                table: "Movies");

            migrationBuilder.AddColumn<int>(
                name: "DirectorId",
                schema: "dbo",
                table: "Movies",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "dbo",
                table: "Directors",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                schema: "dbo",
                table: "Directors",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Directors",
                schema: "dbo",
                table: "Directors",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_DirectorId",
                schema: "dbo",
                table: "Movies",
                column: "DirectorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Directors_DirectorId",
                schema: "dbo",
                table: "Movies",
                column: "DirectorId",
                principalSchema: "dbo",
                principalTable: "Directors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Directors_DirectorId",
                schema: "dbo",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_DirectorId",
                schema: "dbo",
                table: "Movies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Directors",
                schema: "dbo",
                table: "Directors");

            migrationBuilder.DropColumn(
                name: "DirectorId",
                schema: "dbo",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "dbo",
                table: "Directors");

            migrationBuilder.AddColumn<string>(
                name: "DirectorName",
                schema: "dbo",
                table: "Movies",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "dbo",
                table: "Directors",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Directors",
                schema: "dbo",
                table: "Directors",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_DirectorName",
                schema: "dbo",
                table: "Movies",
                column: "DirectorName");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Directors_DirectorName",
                schema: "dbo",
                table: "Movies",
                column: "DirectorName",
                principalSchema: "dbo",
                principalTable: "Directors",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
