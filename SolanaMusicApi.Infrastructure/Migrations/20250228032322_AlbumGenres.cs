using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SolanaMusicApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AlbumGenres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AlbumGenres",
                columns: table => new
                {
                    AlbumsId = table.Column<long>(type: "bigint", nullable: false),
                    GenresId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlbumGenres", x => new { x.AlbumsId, x.GenresId });
                    table.ForeignKey(
                        name: "FK_AlbumGenres_Albums_AlbumsId",
                        column: x => x.AlbumsId,
                        principalTable: "Albums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlbumGenres_Genres_GenresId",
                        column: x => x.GenresId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlbumGenres_GenresId",
                table: "AlbumGenres",
                column: "GenresId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlbumGenres");
        }
    }
}
