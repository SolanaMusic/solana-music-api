using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SolanaMusicApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LikedNftsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LikedNfts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    NftId = table.Column<long>(type: "bigint", nullable: true),
                    CollectionId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikedNfts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LikedNfts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LikedNfts_NftCollections_CollectionId",
                        column: x => x.CollectionId,
                        principalTable: "NftCollections",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LikedNfts_Nfts_NftId",
                        column: x => x.NftId,
                        principalTable: "Nfts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_LikedNfts_CollectionId",
                table: "LikedNfts",
                column: "CollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_LikedNfts_NftId",
                table: "LikedNfts",
                column: "NftId");

            migrationBuilder.CreateIndex(
                name: "IX_LikedNfts_UserId",
                table: "LikedNfts",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LikedNfts");
        }
    }
}
