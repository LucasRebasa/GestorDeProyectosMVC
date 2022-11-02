using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestorDeProyectosMVC.Models
{
    public class Campo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public TipoTarjeta Tipo { get; set; }
        public string Contenido { get; set; }
        public string Usuario { get; set; }
    }
}