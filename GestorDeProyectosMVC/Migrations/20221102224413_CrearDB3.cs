using Microsoft.EntityFrameworkCore.Migrations;

namespace GestorDeProyectosMVC.Migrations
{
    public partial class CrearDB3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Usuario",
                table: "tarjetas");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "tarjetas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_tarjetas_UsuarioId",
                table: "tarjetas",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_tarjetas_usuarios_UsuarioId",
                table: "tarjetas",
                column: "UsuarioId",
                principalTable: "usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tarjetas_usuarios_UsuarioId",
                table: "tarjetas");

            migrationBuilder.DropIndex(
                name: "IX_tarjetas_UsuarioId",
                table: "tarjetas");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "tarjetas");

            migrationBuilder.AddColumn<string>(
                name: "Usuario",
                table: "tarjetas",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
