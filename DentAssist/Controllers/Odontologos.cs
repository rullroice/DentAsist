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
    public class OdontologosController : Controller
    {
        private readonly AppDbContext _context;

        public OdontologosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Odontologos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Odontologos.ToListAsync());
        }

        // GET: Odontologos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Incluye los turnos y planes creados por este odontólogo
            var odontologo = await _context.Odontologos
                .Include(o => o.Turnos)
                    .ThenInclude(t => t.Paciente) // Incluye el paciente del turno
                .Include(o => o.PlanesDeTratamiento)
                    .ThenInclude(pt => pt.Paciente) // Incluye el paciente del plan
                .FirstOrDefaultAsync(m => m.Id == id);

            if (odontologo == null)
            {
                return NotFound();
            }

            return View(odontologo);
        }

        // GET: Odontologos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Odontologos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Matricula,Especialidad,Email")] Odontologo odontologo)
        {
            if (ModelState.IsValid)
            {
                // Verifica si ya existe un odontólogo con la misma Matrícula
                if (await _context.Odontologos.AnyAsync(o => o.Matricula == odontologo.Matricula))
                {
                    ModelState.AddModelError("Matricula", "Ya existe un odontólogo con esta matrícula.");
                    return View(odontologo);
                }

                _context.Add(odontologo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(odontologo);
        }

        // GET: Odontologos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var odontologo = await _context.Odontologos.FindAsync(id);
            if (odontologo == null)
            {
                return NotFound();
            }
            return View(odontologo);
        }

        // POST: Odontologos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Matricula,Especialidad,Email")] Odontologo odontologo)
        {
            if (id != odontologo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Verifica si otro odontólogo ya usa la Matrícula modificada
                    if (await _context.Odontologos.AnyAsync(o => o.Matricula == odontologo.Matricula && o.Id != odontologo.Id))
                    {
                        ModelState.AddModelError("Matricula", "Ya existe otro odontólogo con esta matrícula.");
                        return View(odontologo);
                    }

                    _context.Update(odontologo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OdontologoExists(odontologo.Id))
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
            return View(odontologo);
        }

        // GET: Odontologos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var odontologo = await _context.Odontologos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (odontologo == null)
            {
                return NotFound();
            }

            return View(odontologo);
        }

        // POST: Odontologos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var odontologo = await _context.Odontologos.FindAsync(id);
            if (odontologo != null)
            {
                // Considera la lógica de eliminación en cascada si hay turnos o planes asociados
                // Si tienes DeleteBehavior.Restrict (por defecto), deberás eliminar los relacionados primero.
                _context.Odontologos.Remove(odontologo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OdontologoExists(int id)
        {
            return _context.Odontologos.Any(e => e.Id == id);
        }

        // GET: Odontologos/Agenda/5
        // Visualización de la agenda semanal por profesional
        public async Task<IActionResult> Agenda(int? id, DateTime? startDate)
        {
            if (id == null)
            {
                return NotFound();
            }

            var odontologo = await _context.Odontologos.FindAsync(id);
            if (odontologo == null)
            {
                return NotFound();
            }

            // Calcula la semana de inicio, si no se proporciona, usa la semana actual
            DateTime startOfWeek = startDate?.Date ?? DateTime.Today;
            // Ajusta al lunes de la semana actual (o la semana proporcionada)
            while (startOfWeek.DayOfWeek != DayOfWeek.Monday)
            {
                startOfWeek = startOfWeek.AddDays(-1);
            }
            DateTime endOfWeek = startOfWeek.AddDays(7);

            var turnos = await _context.Turnos
                .Where(t => t.IdOdontologo == id && t.FechaHora >= startOfWeek && t.FechaHora < endOfWeek)
                .Include(t => t.Paciente) // Para mostrar el nombre del paciente en la agenda
                .OrderBy(t => t.FechaHora)
                .ToListAsync();

            ViewBag.Odontologo = odontologo;
            ViewBag.StartOfWeek = startOfWeek;
            ViewBag.EndOfWeek = endOfWeek.AddDays(-1); // Para mostrar el domingo como fin de semana
            return View(turnos); // La vista "Agenda.cshtml" necesitará procesar esta lista de turnos
        }

        // GET: Odontologos/FichasPacientes/5
        // Acceso a fichas de pacientes con los que ha interactuado (por turnos o planes de tratamiento)
        public async Task<IActionResult> FichasPacientes(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var odontologo = await _context.Odontologos.FindAsync(id);
            if (odontologo == null)
            {
                return NotFound();
            }

            // Obtener pacientes únicos asociados a turnos o planes de tratamiento de este odontólogo
            var pacientesPorTurnos = await _context.Turnos
                .Where(t => t.IdOdontologo == id)
                .Select(t => t.Paciente)
                .Distinct()
                .ToListAsync();

            var pacientesPorPlanes = await _context.PlanesDeTratamiento
                .Where(pt => pt.IdOdontologo == id)
                .Select(pt => pt.Paciente)
                .Distinct()
                .ToListAsync();

            var pacientesRelacionados = pacientesPorTurnos.Union(pacientesPorPlanes).OrderBy(p => p.Nombre).ToList();

            ViewBag.Odontologo = odontologo;
            return View(pacientesRelacionados); // La vista "FichasPacientes.cshtml" mostrará esta lista de pacientes
        }
    }
}