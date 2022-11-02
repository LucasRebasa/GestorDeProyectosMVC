using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GestorDeProyectosMVC.Models
{
    public class Proyecto
    {

       
        public int Id { get; set; }
        public string Titulo { get; set; }
        public List<Tarjeta> Tarjetas { get; set; }
        public bool EsVisible { get; set; }
        // virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
