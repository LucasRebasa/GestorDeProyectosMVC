﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestorDeProyectosMVC.Context;
using GestorDeProyectosMVC.Models;
using GestorDeProyectosMVC.ViewModels;

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
            return View(await _context.proyectos.ToListAsync());
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
            ViewBag.usuarios = new MultiSelectList(_context.usuarios, "Id", "Nombre");
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
                /*
                _context.Add(proyecto);
                foreach (var usuarioId in vistaProyecto.Usuarios)
                {
                    var z = proyecto;
                    var up = new UsuarioProyecto(usuarioId, 2);

                }
                var usuariosDb = _context.usuarioProyectos.Where(up => up.ProyectoId == 2);
                */
                //TODO: Crear el proyecto sin usuarios asociados y crear un boton de añadir usuarios a un proyecto
                _context.proyectos.Add(proyecto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(proyecto);
        }

        // GET: Proyecto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.usuarios = new MultiSelectList(_context.usuarios, "Id", "Nombre");
            if (id == null)
            {
                return NotFound();
            }

            var proyecto = await _context.proyectos.FindAsync(id);
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
            if(vistaProyecto.Usuarios.Count > 0)
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

    }
}
