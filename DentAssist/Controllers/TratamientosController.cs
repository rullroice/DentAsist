using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DentAssist.Data;
using DentAssist.Models;

namespace DentAssist.Controllers
{
    public class TratamientosController : Controller
    {
        private readonly AppDbContext _context;

        public TratamientosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Tratamientos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tratamientos.ToListAsync());
        }

        // GET: Tratamientos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tratamiento = await _context.Tratamientos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tratamiento == null)
            {
                return NotFound();
            }

            return View(tratamiento);
        }

        // GET: Tratamientos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tratamientos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Descripcion,PrecioEstimado")] Tratamiento tratamiento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tratamiento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tratamiento);
        }

        // GET: Tratamientos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tratamiento = await _context.Tratamientos.FindAsync(id);
            if (tratamiento == null)
            {
                return NotFound();
            }
            return View(tratamiento);
        }

        // POST: Tratamientos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Descripcion,PrecioEstimado")] Tratamiento tratamiento)
        {
            if (id != tratamiento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tratamiento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TratamientoExists(tratamiento.Id))
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
            return View(tratamiento);
        }

        // GET: Tratamientos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tratamiento = await _context.Tratamientos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tratamiento == null)
            {
                return NotFound();
            }

            return View(tratamiento);
        }

        // POST: Tratamientos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tratamiento = await _context.Tratamientos.FindAsync(id);
            if (tratamiento != null)
            {
                // Considera si este tratamiento está referenciado en algún PasoTratamiento.
                // Si tienes DeleteBehavior.Restrict (por defecto), la DB no permitirá la eliminación.
                _context.Tratamientos.Remove(tratamiento);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TratamientoExists(int id)
        {
            return _context.Tratamientos.Any(e => e.Id == id);
        }
    }
}