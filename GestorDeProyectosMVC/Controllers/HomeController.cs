using GestorDeProyectosMVC.Context;
using GestorDeProyectosMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GestorDeProyectosMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly GestorProyectosDBContext _context;

        public HomeController(ILogger<HomeController> logger, GestorProyectosDBContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Ingresar([Bind("Nombre","Contrasenia")] Usuario usuario)
        {
            if (!_context.usuarios.Any(u => u.Nombre == usuario.Nombre && u.Contrasenia == usuario.Contrasenia))
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index","Proyecto");
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear([Bind("Nombre","Contrasenia")] Usuario usuario)
        {
            if(_context.usuarios.Any(u => u.Nombre == usuario.Nombre))
            {
                return RedirectToAction("Crear");
            }
            _context.usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index","Proyecto");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
