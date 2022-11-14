using GestorDeProyectosMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestorDeProyectosMVC.ViewModels
{
    public class ViewProyecto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public List<Tarjeta> Tarjetas { get; set; }
        public bool EsVisible { get; set; }
        public ICollection<UsuarioProyecto> usuarioProyecto { get; set; }
        public List<int> Usuarios { get; set; }

    }
}
