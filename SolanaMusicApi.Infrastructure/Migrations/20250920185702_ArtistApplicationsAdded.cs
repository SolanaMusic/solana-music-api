using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SolanaMusicApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ArtistApplicationsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArtistApplications",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    ResourceUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ContactLink = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ReviewerId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtistApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArtistApplications_AspNetUsers_ReviewerId",
                        column: x => x.ReviewerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ArtistApplications_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArtistApplications_CreatedDate",
                table: "ArtistApplications",
                column: "CreatedDate");

            migrationBuilder.CreateIndex(
                name: "IX_ArtistApplications_ReviewerId",
                table: "ArtistApplications",
                column: "ReviewerId");

            migrationBuilder.CreateIndex(
                name: "IX_ArtistApplications_Status",
                table: "ArtistApplications",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_ArtistApplications_UserId",
                table: "ArtistApplications",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtistApplications");
        }
    }
}
