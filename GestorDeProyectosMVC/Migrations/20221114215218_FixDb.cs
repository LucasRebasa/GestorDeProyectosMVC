using Microsoft.EntityFrameworkCore.Migrations;

namespace GestorDeProyectosMVC.Migrations
{
    public partial class FixDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioProyecto_proyectos_ProyectoId",
                table: "UsuarioProyecto");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioProyecto_usuarios_UsuarioId",
                table: "UsuarioProyecto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuarioProyecto",
                table: "UsuarioProyecto");

            migrationBuilder.RenameTable(
                name: "UsuarioProyecto",
                newName: "usuarioProyectos");

            migrationBuilder.RenameIndex(
                name: "IX_UsuarioProyecto_ProyectoId",
                table: "usuarioProyectos",
                newName: "IX_usuarioProyectos_ProyectoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_usuarioProyectos",
                table: "usuarioProyectos",
                columns: new[] { "UsuarioId", "ProyectoId" });

            migrationBuilder.AddForeignKey(
                name: "FK_usuarioProyectos_proyectos_ProyectoId",
                table: "usuarioProyectos",
                column: "ProyectoId",
                principalTable: "proyectos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_usuarioProyectos_usuarios_UsuarioId",
                table: "usuarioProyectos",
                column: "UsuarioId",
                principalTable: "usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_usuarioProyectos_proyectos_ProyectoId",
                table: "usuarioProyectos");

            migrationBuilder.DropForeignKey(
                name: "FK_usuarioProyectos_usuarios_UsuarioId",
                table: "usuarioProyectos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_usuarioProyectos",
                table: "usuarioProyectos");

            migrationBuilder.RenameTable(
                name: "usuarioProyectos",
                newName: "UsuarioProyecto");

            migrationBuilder.RenameIndex(
                name: "IX_usuarioProyectos_ProyectoId",
                table: "UsuarioProyecto",
                newName: "IX_UsuarioProyecto_ProyectoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuarioProyecto",
                table: "UsuarioProyecto",
                columns: new[] { "UsuarioId", "ProyectoId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioProyecto_proyectos_ProyectoId",
                table: "UsuarioProyecto",
                column: "ProyectoId",
                principalTable: "proyectos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioProyecto_usuarios_UsuarioId",
                table: "UsuarioProyecto",
                column: "UsuarioId",
                principalTable: "usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
