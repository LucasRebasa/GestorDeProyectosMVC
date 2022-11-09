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


        public DbSet<Proyecto> proyectos { get; set; }
        public DbSet<Tarjeta> tarjetas { get; set; }
        public DbSet<Campo> campos { get; set; }
        public DbSet<Usuario> usuarios { get; set; }
    }
}
