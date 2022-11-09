using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestorDeProyectosMVC.Models
{
    public class UsuarioProyecto
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public int ProyectoId { get; set; }
        public Proyecto Proyecto { get; set; }
    }
}
