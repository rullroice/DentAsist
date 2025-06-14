using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DentAssist.Data;
using DentAssist.Models;
using Microsoft.Extensions.Logging; // Agregado para el logging

namespace DentAssist.Controllers
{
    public class TurnosController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<TurnosController> _logger; // Inyección del logger

        public TurnosController(AppDbContext context, ILogger<TurnosController> logger) // Añadir ILogger al constructor
        {
            _context = context;
            _logger = logger;
        }

        // GET: Turnos
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Turnos
                                     .Include(t => t.Paciente)
                                     .Include(t => t.Odontologo)
                                     .OrderBy(t => t.FechaHora);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Turnos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turno = await _context.Turnos
                .Include(t => t.Paciente)
                .Include(t => t.Odontologo)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (turno == null)
            {
                return NotFound();
            }

            return View(turno);
        }

        // GET: Turnos/Create
        public IActionResult Create()
        {
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "Id", "Nombre");
            ViewData["IdOdontologo"] = new SelectList(_context.Odontologos, "Id", "Nombre");
            return View(new Turno { FechaHora = DateTime.Now });
        }

        // POST: Turnos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        // **CORRECCIÓN CLAVE:** Asegurarse que "DuracionMinutos" esté en el atributo [Bind]
        public async Task<IActionResult> Create([Bind("Id,IdPaciente,IdOdontologo,FechaHora,DuracionMinutos,Estado,Descripcion")] Turno turno)
        {
            // Registrar el valor de DuracionMinutos ANTES de la validación
            _logger.LogInformation("Create (POST) - DuracionMinutos recibido: {DuracionMinutos}", turno.DuracionMinutos);

            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(turno.Estado))
                {
                    turno.Estado = "Programado"; // Asumo "Programado" como estado por defecto en la creación
                }
                _context.Add(turno);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Turno creado exitosamente con ID: {TurnoId}", turno.Id);
                return RedirectToAction(nameof(Index));
            }

            // Si ModelState es inválido, registrar los errores para depuración
            _logger.LogWarning("Create (POST) - ModelState es inválido para Turno. Errores:");
            foreach (var state in ModelState)
            {
                if (state.Value.Errors.Any())
                {
                    foreach (var error in state.Value.Errors)
                    {
                        _logger.LogError("Campo: {FieldName}, Error: {ErrorMessage}", state.Key, error.ErrorMessage);
                    }
                }
            }

            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "Id", "Nombre", turno.IdPaciente);
            ViewData["IdOdontologo"] = new SelectList(_context.Odontologos, "Id", "Nombre", turno.IdOdontologo);
            return View(turno);
        }

        // GET: Turnos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turno = await _context.Turnos
                                  .Include(t => t.Paciente)
                                  .Include(t => t.Odontologo)
                                  .FirstOrDefaultAsync(m => m.Id == id);

            if (turno == null)
            {
                return NotFound();
            }

            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "Id", "Nombre", turno.IdPaciente);
            ViewData["IdOdontologo"] = new SelectList(_context.Odontologos, "Id", "Nombre", turno.IdOdontologo);
            return View(turno);
        }

        // POST: Turnos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        // **CORRECCIÓN CLAVE:** Asegurarse que "DuracionMinutos" esté en el atributo [Bind]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdPaciente,IdOdontologo,FechaHora,DuracionMinutos,Estado,Descripcion")] Turno turno)
        {
            // Registrar el valor de DuracionMinutos ANTES de la validación
            _logger.LogInformation("Edit (POST) - DuracionMinutos recibido: {DuracionMinutos}", turno.DuracionMinutos);

            if (id != turno.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(turno);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Turno actualizado exitosamente con ID: {TurnoId}", turno.Id);
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    _logger.LogError(ex, "Error de concurrencia al actualizar Turno ID: {TurnoId}", turno.Id);
                    if (!TurnoExists(turno.Id))
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

            // Si ModelState es inválido, registrar los errores para depuración
            _logger.LogWarning("Edit (POST) - ModelState es inválido para Turno. Errores:");
            foreach (var state in ModelState)
            {
                if (state.Value.Errors.Any())
                {
                    foreach (var error in state.Value.Errors)
                    {
                        _logger.LogError("Campo: {FieldName}, Error: {ErrorMessage}", state.Key, error.ErrorMessage);
                    }
                }
            }

            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "Id", "Nombre", turno.IdPaciente);
            ViewData["IdOdontologo"] = new SelectList(_context.Odontologos, "Id", "Nombre", turno.IdOdontologo);
            return View(turno);
        }

        // GET: Turnos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turno = await _context.Turnos
                .Include(t => t.Paciente)
                .Include(t => t.Odontologo)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (turno == null)
            {
                return NotFound();
            }

            return View(turno);
        }

        // POST: Turnos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var turno = await _context.Turnos.FindAsync(id);
            if (turno != null)
            {
                _context.Turnos.Remove(turno);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Turno ID: {TurnoId} eliminado exitosamente.", id);
            }
            else
            {
                _logger.LogWarning("Intento de eliminar Turno ID: {TurnoId} fallido, no encontrado.", id);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool TurnoExists(int id)
        {
            return _context.Turnos.Any(e => e.Id == id);
        }
    }
}