using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaStreaming.Migrations
{
    /// <inheritdoc />
    public partial class addCampoSalt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Salt",
                table: "Usuario",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Salt",
                table: "Usuario");
        }
    }
}
