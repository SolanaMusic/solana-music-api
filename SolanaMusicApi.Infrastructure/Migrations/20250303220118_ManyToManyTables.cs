using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SolanaMusicApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ManyToManyTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArtistTracks_Artists_ArtistsId",
                table: "ArtistTracks");

            migrationBuilder.DropForeignKey(
                name: "FK_ArtistTracks_Tracks_TracksId",
                table: "ArtistTracks");

            migrationBuilder.DropForeignKey(
                name: "FK_TrackGenres_Genres_GenresId",
                table: "TrackGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_TrackGenres_Tracks_TracksId",
                table: "TrackGenres");

            migrationBuilder.RenameColumn(
                name: "TracksId",
                table: "TrackGenres",
                newName: "GenreId");

            migrationBuilder.RenameColumn(
                name: "GenresId",
                table: "TrackGenres",
                newName: "TrackId");

            migrationBuilder.RenameIndex(
                name: "IX_TrackGenres_TracksId",
                table: "TrackGenres",
                newName: "IX_TrackGenres_GenreId");

            migrationBuilder.RenameColumn(
                name: "TracksId",
                table: "ArtistTracks",
                newName: "ArtistId");

            migrationBuilder.RenameColumn(
                name: "ArtistsId",
                table: "ArtistTracks",
                newName: "TrackId");

            migrationBuilder.RenameIndex(
                name: "IX_ArtistTracks_TracksId",
                table: "ArtistTracks",
                newName: "IX_ArtistTracks_ArtistId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistTracks_Artists_ArtistId",
                table: "ArtistTracks",
                column: "ArtistId",
                principalTable: "Artists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistTracks_Tracks_TrackId",
                table: "ArtistTracks",
                column: "TrackId",
                principalTable: "Tracks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrackGenres_Genres_GenreId",
                table: "TrackGenres",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrackGenres_Tracks_TrackId",
                table: "TrackGenres",
                column: "TrackId",
                principalTable: "Tracks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArtistTracks_Artists_ArtistId",
                table: "ArtistTracks");

            migrationBuilder.DropForeignKey(
                name: "FK_ArtistTracks_Tracks_TrackId",
                table: "ArtistTracks");

            migrationBuilder.DropForeignKey(
                name: "FK_TrackGenres_Genres_GenreId",
                table: "TrackGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_TrackGenres_Tracks_TrackId",
                table: "TrackGenres");

            migrationBuilder.RenameColumn(
                name: "GenreId",
                table: "TrackGenres",
                newName: "TracksId");

            migrationBuilder.RenameColumn(
                name: "TrackId",
                table: "TrackGenres",
                newName: "GenresId");

            migrationBuilder.RenameIndex(
                name: "IX_TrackGenres_GenreId",
                table: "TrackGenres",
                newName: "IX_TrackGenres_TracksId");

            migrationBuilder.RenameColumn(
                name: "ArtistId",
                table: "ArtistTracks",
                newName: "TracksId");

            migrationBuilder.RenameColumn(
                name: "TrackId",
                table: "ArtistTracks",
                newName: "ArtistsId");

            migrationBuilder.RenameIndex(
                name: "IX_ArtistTracks_ArtistId",
                table: "ArtistTracks",
                newName: "IX_ArtistTracks_TracksId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistTracks_Artists_ArtistsId",
                table: "ArtistTracks",
                column: "ArtistsId",
                principalTable: "Artists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistTracks_Tracks_TracksId",
                table: "ArtistTracks",
                column: "TracksId",
                principalTable: "Tracks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrackGenres_Genres_GenresId",
                table: "TrackGenres",
                column: "GenresId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrackGenres_Tracks_TracksId",
                table: "TrackGenres",
                column: "TracksId",
                principalTable: "Tracks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
