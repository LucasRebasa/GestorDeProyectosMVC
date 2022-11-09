using GestorDeProyectosMVC.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestorDeProyectosMVC.Context
{
    public class GestorProyectosDBContext: DbContext
    {
        public GestorProyectosDBContext(DbContextOptions<GestorProyectosDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsuarioProyecto>()
                .HasKey(up => new { up.UsuarioId, up.ProyectoId });
            modelBuilder.Entity<UsuarioProyecto>()
                .HasOne(up => up.Proyecto)
                .WithMany(p => p.UsuariosProyectos)
                .HasForeignKey(up => up.ProyectoId);
            modelBuilder.Entity<UsuarioProyecto>()
                .HasOne(up => up.Usuario)
                .WithMany(u => u.UsuariosProyectos)
                .HasForeignKey(up => up.UsuarioId);
        }


        public DbSet<Proyecto> proyectos { get; set; }
        public DbSet<Tarjeta> tarjetas { get; set; }
        public DbSet<Campo> campos { get; set; }
        public DbSet<Usuario> usuarios { get; set; }

        public DbSet<UsuarioProyecto> usuarioProyectos { get; set; }
    }
}
