using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestorDeProyectosMVC.Models
{
    public class Campo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [EnumDataType(typeof(TipoTarjeta))]
        public TipoTarjeta Tipo { get; set; }
        public string Contenido { get; set; }
        public string Usuario { get; set; }

        public int TarjetaId { get; set; }
        public virtual Tarjeta Tarjeta { get; set; }
    }
}