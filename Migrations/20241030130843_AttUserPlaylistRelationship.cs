using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaStreaming.Migrations
{
    /// <inheritdoc />
    public partial class AttUserPlaylistRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Playlist_Usuario_UserId1",
                table: "Playlist");

            migrationBuilder.DropIndex(
                name: "IX_Playlist_UserId1",
                table: "Playlist");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Playlist");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Playlist",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Playlist_UserId",
                table: "Playlist",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Playlist_Usuario_UserId",
                table: "Playlist",
                column: "UserId",
                principalTable: "Usuario",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Playlist_Usuario_UserId",
                table: "Playlist");

            migrationBuilder.DropIndex(
                name: "IX_Playlist_UserId",
                table: "Playlist");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Playlist");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "Playlist",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Playlist_UserId1",
                table: "Playlist",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Playlist_Usuario_UserId1",
                table: "Playlist",
                column: "UserId1",
                principalTable: "Usuario",
                principalColumn: "UserId");
        }
    }
}
