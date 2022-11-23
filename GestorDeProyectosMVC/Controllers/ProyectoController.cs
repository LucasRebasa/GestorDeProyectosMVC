using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestorDeProyectosMVC.Context;
using GestorDeProyectosMVC.Models;
using GestorDeProyectosMVC.ViewModels;
using Microsoft.AspNetCore.Http;

namespace GestorDeProyectosMVC.Controllers
{
    public class ProyectoController : Controller
    {
        private readonly GestorProyectosDBContext _context;

        public ProyectoController(GestorProyectosDBContext context)
        {
            _context = context;
        }

        // GET: Proyecto
        public async Task<IActionResult> Index()
        {
            
            string nomusuario = HttpContext.Session.GetString("Usuario");
            if(nomusuario == "")
            {
                return RedirectToAction("Index", "Home");
            }
            //Se seleccionan todos los Id's de los proyectos donde este el usuario
            var ups = _context.usuarioProyectos.Where(up => up.Usuario.Nombre == nomusuario).Select(up => up.ProyectoId).ToList();
            //Se filtran todos los proyectos seleccionando solo los proyectos (objeto Proyecto) donde este el usuario
            var list = await _context.proyectos.Where(p => ups.Contains(p.Id)).ToListAsync();

            return View(list);
        }
        
        // GET: Proyecto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proyecto = await _context.proyectos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (proyecto == null)
            {
                return NotFound();
            }

            return View(proyecto);
        }

        // GET: Proyecto/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Proyecto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,EsVisible")] Proyecto proyecto)
        {

            if (ModelState.IsValid)
            {
                var nomSession = HttpContext.Session.GetString("Usuario");
                var usuario = _context.usuarios.First(u => u.Nombre == nomSession);
                var proyectoDB = _context.proyectos.Add(proyecto);
                await _context.SaveChangesAsync();
                _context.usuarioProyectos.Add(new UsuarioProyecto(usuario.Id, proyectoDB.Entity.Id));
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(proyecto);
        }

        // GET: Proyecto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //Seleccionar todos los usuarios del proyecto
            var usuariosDelProyecto = _context.usuarioProyectos.Where(up => up.ProyectoId == id).Select(up => up.UsuarioId);
            var proyecto = await _context.proyectos.FindAsync(id);
            //Filtra los usuarios eliminando los que ya pertenezcan al proyecto y mostrando todos los demas
            var usuariosAMostrar =  _context.usuarios.Where(u => !usuariosDelProyecto.Contains(u.Id)).ToList();
            ViewBag.usuarios = new MultiSelectList(usuariosAMostrar, "Id", "Nombre");
            var vista = new ViewProyecto
            {
                Id = proyecto.Id,
                Titulo = proyecto.Titulo,
                EsVisible = proyecto.EsVisible,
                Tarjetas = proyecto.Tarjetas,
                usuarioProyecto = proyecto.UsuariosProyectos
            };
            if (proyecto == null)
            {
                return NotFound();
            }
            return View(vista);
        }

        // POST: Proyecto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ViewProyecto vistaProyecto)
        {
            //TODO: mostrar los usuarios que ya estan en el proyecto y excluirlos de la seleccion
            Proyecto proyecto = new Proyecto
            {
                Id = vistaProyecto.Id,
                Titulo = vistaProyecto.Titulo,
                EsVisible = vistaProyecto.EsVisible
            };
            if(vistaProyecto.Usuarios != null)
            {
                foreach (var usuarioId in vistaProyecto.Usuarios)
                {
                    _context.usuarioProyectos.Add(new UsuarioProyecto(usuarioId,id));
                }
            }
            if (id != proyecto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proyecto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProyectoExists(proyecto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(proyecto);
        }

        // GET: Proyecto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proyecto = await _context.proyectos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (proyecto == null)
            {
                return NotFound();
            }

            return View(proyecto);
        }

        // POST: Proyecto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var proyecto = await _context.proyectos.FindAsync(id);
            _context.proyectos.Remove(proyecto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProyectoExists(int id)
        {
            return _context.proyectos.Any(e => e.Id == id);
        }

        public IActionResult CreateTarjeta(int? id)
        {
            return RedirectToAction("Create","Tarjeta",new { idProyecto = id });
        }

        public async Task<IActionResult> Abrir(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            Proyecto proyecto = await _context.proyectos.FindAsync(id);
            List<Tarjeta> tarjetas = _context.tarjetas.Where(t => t.ProyectoId == id).ToList();
            proyecto.Tarjetas = tarjetas;
            foreach(var tarjeta in proyecto.Tarjetas)
            {
                tarjeta.Campos = _context.campos.Where(c => c.TarjetaId == tarjeta.Id).ToList();
            }
            return View(proyecto);
        }

    }
}
