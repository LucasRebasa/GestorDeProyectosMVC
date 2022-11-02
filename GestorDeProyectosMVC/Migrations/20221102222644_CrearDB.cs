using Microsoft.EntityFrameworkCore.Migrations;

namespace GestorDeProyectosMVC.Migrations
{
    public partial class CrearDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "proyectos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(nullable: true),
                    EsVisible = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_proyectos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(nullable: true),
                    Contrasenia = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tarjetas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(nullable: true),
                    Contenido = table.Column<string>(nullable: true),
                    Usuario = table.Column<string>(nullable: true),
                    ProyectoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tarjetas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tarjetas_proyectos_ProyectoId",
                        column: x => x.ProyectoId,
                        principalTable: "proyectos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "campos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<int>(nullable: false),
                    Contenido = table.Column<string>(nullable: true),
                    Usuario = table.Column<string>(nullable: true),
                    TarjetaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_campos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_campos_tarjetas_TarjetaId",
                        column: x => x.TarjetaId,
                        principalTable: "tarjetas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_campos_TarjetaId",
                table: "campos",
                column: "TarjetaId");

            migrationBuilder.CreateIndex(
                name: "IX_tarjetas_ProyectoId",
                table: "tarjetas",
                column: "ProyectoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "campos");

            migrationBuilder.DropTable(
                name: "usuarios");

            migrationBuilder.DropTable(
                name: "tarjetas");

            migrationBuilder.DropTable(
                name: "proyectos");
        }
    }
}
