using Microsoft.EntityFrameworkCore.Migrations;

namespace GestorDeProyectosMVC.Migrations
{
    public partial class RelacionProyectoUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsuarioProyecto",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(nullable: false),
                    ProyectoId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioProyecto", x => new { x.UsuarioId, x.ProyectoId });
                    table.ForeignKey(
                        name: "FK_UsuarioProyecto_proyectos_ProyectoId",
                        column: x => x.ProyectoId,
                        principalTable: "proyectos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioProyecto_usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioProyecto_ProyectoId",
                table: "UsuarioProyecto",
                column: "ProyectoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsuarioProyecto");
        }
    }
}
