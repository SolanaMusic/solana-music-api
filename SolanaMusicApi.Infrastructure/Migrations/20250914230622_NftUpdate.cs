using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SolanaMusicApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NftUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Nfts",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Nfts_UserId",
                table: "Nfts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Nfts_AspNetUsers_UserId",
                table: "Nfts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nfts_AspNetUsers_UserId",
                table: "Nfts");

            migrationBuilder.DropIndex(
                name: "IX_Nfts_UserId",
                table: "Nfts");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Nfts");
        }
    }
}
