using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestorDeProyectosMVC.Models
{
    public class Tarjeta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public List<Campo> Campos { get; set; }


        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }

        public int ProyectoId { get; set; }
        public virtual Proyecto Proyecto { get; set; }
    }
}