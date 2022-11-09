using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GestorDeProyectosMVC.Models
{
    public class Usuario
    {
     


        public int Id { get; set; }
        public string Nombre { get; set; }
        [Display(Name = "Contraseña")]
        public string Contrasenia { get; set; }
        public ICollection<UsuarioProyecto> UsuariosProyectos { get; set; }

    }
}
