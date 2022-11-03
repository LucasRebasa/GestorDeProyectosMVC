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
    public class CampoController : Controller
    {
        private readonly GestorProyectosDBContext _context;

        public CampoController(GestorProyectosDBContext context)
        {
            _context = context;
        }

        // GET: Campo
        public async Task<IActionResult> Index()
        {
            var gestorProyectosDBContext = _context.campos.Include(c => c.Tarjeta);
            return View(await gestorProyectosDBContext.ToListAsync());
        }

        // GET: Campo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campo = await _context.campos
                .Include(c => c.Tarjeta)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (campo == null)
            {
                return NotFound();
            }

            return View(campo);
        }

        // GET: Campo/Create
        public IActionResult Create()
        {
            ViewData["TarjetaId"] = new SelectList(_context.tarjetas, "Id", "Id");
            return View();
        }

        // POST: Campo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Tipo,Contenido,Usuario,TarjetaId")] Campo campo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(campo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TarjetaId"] = new SelectList(_context.tarjetas, "Id", "Id", campo.TarjetaId);
            return View(campo);
        }

        // GET: Campo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campo = await _context.campos.FindAsync(id);
            if (campo == null)
            {
                return NotFound();
            }
            ViewData["TarjetaId"] = new SelectList(_context.tarjetas, "Id", "Id", campo.TarjetaId);
            return View(campo);
        }

        // POST: Campo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Tipo,Contenido,Usuario,TarjetaId")] Campo campo)
        {
            if (id != campo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(campo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CampoExists(campo.Id))
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
            ViewData["TarjetaId"] = new SelectList(_context.tarjetas, "Id", "Id", campo.TarjetaId);
            return View(campo);
        }

        // GET: Campo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campo = await _context.campos
                .Include(c => c.Tarjeta)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (campo == null)
            {
                return NotFound();
            }

            return View(campo);
        }

        // POST: Campo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var campo = await _context.campos.FindAsync(id);
            _context.campos.Remove(campo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CampoExists(int id)
        {
            return _context.campos.Any(e => e.Id == id);
        }
    }
}
