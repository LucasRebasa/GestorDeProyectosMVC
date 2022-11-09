using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestorDeProyectosMVC.Context;
using GestorDeProyectosMVC.Models;

namespace GestorDeProyectosMVC.Controllers
{
    public class TarjetaController : Controller
    {
        private readonly GestorProyectosDBContext _context;

        public TarjetaController(GestorProyectosDBContext context)
        {
            _context = context;
        }

        // GET: Tarjeta
        public async Task<IActionResult> Index()
        {
            var gestorProyectosDBContext = _context.tarjetas.Include(t => t.Proyecto).Include(t => t.Usuario);
            return View(await gestorProyectosDBContext.ToListAsync());
        }

        // GET: Tarjeta/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarjeta = await _context.tarjetas
                .Include(t => t.Proyecto)
                .Include(t => t.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tarjeta == null)
            {
                return NotFound();
            }

            return View(tarjeta);
        }

        // GET: Tarjeta/Create
        //public IActionResult Create()
        //{
        //    ViewData["ProyectoId"] = new SelectList(_context.proyectos, "Id", "Id");
        //    ViewData["UsuarioId"] = new SelectList(_context.usuarios, "Id", "Id");
        //    return View();
        //}
        public IActionResult Create(int idProyecto)
        {
            ViewData["UsuarioId"] = new SelectList(_context.usuarios, "Id", "Nombre");
            ViewBag.ProyectosId = idProyecto;
            return View();
        }

        public IActionResult CreateCampo(int id)
        {
            return RedirectToAction("Create", "Campo", new { idTarjeta = id });
        }

        // POST: Tarjeta/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Contenido,UsuarioId,ProyectoId")] Tarjeta tarjeta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tarjeta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProyectoId"] = new SelectList(_context.proyectos, "Id", "Id", tarjeta.ProyectoId);
            ViewData["UsuarioId"] = new SelectList(_context.usuarios, "Id", "Id", tarjeta.UsuarioId);
            return View(tarjeta);
        }

        // GET: Tarjeta/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarjeta = await _context.tarjetas.FindAsync(id);
            if (tarjeta == null)
            {
                return NotFound();
            }
            ViewData["ProyectoId"] = new SelectList(_context.proyectos, "Id", "Id", tarjeta.ProyectoId);
            ViewData["UsuarioId"] = new SelectList(_context.usuarios, "Id", "Id", tarjeta.UsuarioId);
            return View(tarjeta);
        }

        // POST: Tarjeta/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Contenido,UsuarioId,ProyectoId")] Tarjeta tarjeta)
        {
            if (id != tarjeta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tarjeta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TarjetaExists(tarjeta.Id))
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
            ViewData["ProyectoId"] = new SelectList(_context.proyectos, "Id", "Id", tarjeta.ProyectoId);
            ViewData["UsuarioId"] = new SelectList(_context.usuarios, "Id", "Id", tarjeta.UsuarioId);
            return View(tarjeta);
        }

        // GET: Tarjeta/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarjeta = await _context.tarjetas
                .Include(t => t.Proyecto)
                .Include(t => t.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tarjeta == null)
            {
                return NotFound();
            }

            return View(tarjeta);
        }

        // POST: Tarjeta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tarjeta = await _context.tarjetas.FindAsync(id);
            _context.tarjetas.Remove(tarjeta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TarjetaExists(int id)
        {
            return _context.tarjetas.Any(e => e.Id == id);
        }
    }
}
