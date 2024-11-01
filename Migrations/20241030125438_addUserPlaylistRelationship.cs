using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaStreaming.Migrations
{
    /// <inheritdoc />
    public partial class addUserPlaylistRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Playlist",
                columns: table => new
                {
                    PlayListId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId1 = table.Column<Guid>(type: "TEXT", nullable: true),
                    Title = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Playlist", x => x.PlayListId);
                    table.ForeignKey(
                        name: "FK_Playlist_Usuario_UserId1",
                        column: x => x.UserId1,
                        principalTable: "Usuario",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Playlist_UserId1",
                table: "Playlist",
                column: "UserId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Playlist");
        }
    }
}
