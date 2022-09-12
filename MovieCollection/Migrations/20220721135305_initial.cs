using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieCollection.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Directors",
                schema: "dbo",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Directors", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "MovieCollection",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MoviesCount = table.Column<int>(type: "int", nullable: false),
                    DirectorsCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieCollection", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: false),
                    DirectorName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MovieCollectionId = table.Column<int>(type: "int", nullable: false),
                    MoviesCollectionID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Movies_Directors_DirectorName",
                        column: x => x.DirectorName,
                        principalSchema: "dbo",
                        principalTable: "Directors",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Movies_MovieCollection_MoviesCollectionID",
                        column: x => x.MoviesCollectionID,
                        principalSchema: "dbo",
                        principalTable: "MovieCollection",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movies_DirectorName",
                schema: "dbo",
                table: "Movies",
                column: "DirectorName");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_MoviesCollectionID",
                schema: "dbo",
                table: "Movies",
                column: "MoviesCollectionID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movies",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Directors",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "MovieCollection",
                schema: "dbo");
        }
    }
}
