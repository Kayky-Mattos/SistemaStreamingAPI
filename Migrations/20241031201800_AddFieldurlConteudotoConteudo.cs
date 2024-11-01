using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaStreaming.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldurlConteudotoConteudo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "urlConteudo",
                table: "Conteudos",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "urlConteudo",
                table: "Conteudos");
        }
    }
}
