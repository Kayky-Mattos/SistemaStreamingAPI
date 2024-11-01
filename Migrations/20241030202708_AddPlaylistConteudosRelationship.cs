using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaStreaming.Migrations
{
    /// <inheritdoc />
    public partial class AddPlaylistConteudosRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Playlist",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.CreateTable(
                name: "Conteudos",
                columns: table => new
                {
                    ConteudoId = table.Column<Guid>(type: "TEXT", nullable: false),
                    PlayListId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    Type = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conteudos", x => x.ConteudoId);
                    table.ForeignKey(
                        name: "FK_Conteudos_Playlist_PlayListId",
                        column: x => x.PlayListId,
                        principalTable: "Playlist",
                        principalColumn: "PlayListId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Conteudos_PlayListId",
                table: "Conteudos",
                column: "PlayListId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Conteudos");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Playlist",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
        }
    }
}
