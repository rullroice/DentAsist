using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DentAssist.Data;
using DentAssist.Models;
using Rotativa.AspNetCore;

namespace DentAssist.Controllers
{
    public class PlanTratamientosController : Controller
    {
        private readonly AppDbContext _context;

        public PlanTratamientosController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var planes = _context.PlanesDeTratamiento
                                .Include(p => p.Odontologo)
                                .Include(p => p.Paciente);
            return View(await planes.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var planTratamiento = await _context.PlanesDeTratamiento
                .Include(p => p.Paciente)
                .Include(p => p.Odontologo)
                .Include(p => p.PasosDelPlan)
                    .ThenInclude(ps => ps.Tratamiento)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (planTratamiento == null) return NotFound();

            return View(planTratamiento);
        }

        public IActionResult Create()
        {
            ViewData["IdOdontologo"] = new SelectList(_context.Odontologos, "Id", "Nombre");
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "Id", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdPaciente,IdOdontologo,FechaCreacion,ObservacionesGenerales")] PlanTratamiento planTratamiento)
        {
            if (ModelState.IsValid)
            {
                if (planTratamiento.FechaCreacion == DateTime.MinValue)
                {
                    planTratamiento.FechaCreacion = DateTime.Now;
                }

                _context.Add(planTratamiento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = planTratamiento.Id });
            }

            ViewData["IdOdontologo"] = new SelectList(_context.Odontologos, "Id", "Nombre", planTratamiento.IdOdontologo);
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "Id", "Nombre", planTratamiento.IdPaciente);
            return View(planTratamiento);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var planTratamiento = await _context.PlanesDeTratamiento
                .Include(pt => pt.PasosDelPlan)
                    .ThenInclude(ps => ps.Tratamiento)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (planTratamiento == null) return NotFound();

            ViewData["IdOdontologo"] = new SelectList(_context.Odontologos, "Id", "Nombre", planTratamiento.IdOdontologo);
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "Id", "Nombre", planTratamiento.IdPaciente);
            ViewData["Tratamientos"] = new SelectList(_context.Tratamientos, "Id", "Nombre");
            return View(planTratamiento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdPaciente,IdOdontologo,FechaCreacion,ObservacionesGenerales")] PlanTratamiento planTratamiento)
        {
            if (id != planTratamiento.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(planTratamiento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlanTratamientoExists(planTratamiento.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Details), new { id = planTratamiento.Id });
            }

            ViewData["IdOdontologo"] = new SelectList(_context.Odontologos, "Id", "Nombre", planTratamiento.IdOdontologo);
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "Id", "Nombre", planTratamiento.IdPaciente);
            return View(planTratamiento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPaso([Bind("IdPlanTratamiento,IdTratamiento,Secuencia,FechaEstimada,ObservacionesClinicas,Estado")] PasoTratamiento paso)
        {
            if (string.IsNullOrEmpty(paso.Estado))
                paso.Estado = "Pendiente";

            if (!PlanTratamientoExists(paso.IdPlanTratamiento))
                ModelState.AddModelError(string.Empty, "El plan de tratamiento especificado no existe.");

            if (ModelState.IsValid)
            {
                _context.Add(paso);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Edit), new { id = paso.IdPlanTratamiento });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePasoEstado(int id, string nuevoEstado, int idPlanTratamiento)
        {
            var paso = await _context.PasosDeTratamiento.FindAsync(id);
            if (paso == null) return NotFound();

            var estadosValidos = new List<string> { "Pendiente", "Realizado", "Cancelado" };
            if (!estadosValidos.Contains(nuevoEstado)) return BadRequest("Estado de paso no válido.");

            paso.Estado = nuevoEstado;
            paso.FechaRealizado = (nuevoEstado == "Realizado") ? DateTime.Now : null;

            _context.Update(paso);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Edit), new { id = idPlanTratamiento });
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var planTratamiento = await _context.PlanesDeTratamiento
                .Include(p => p.Paciente)
                .Include(p => p.Odontologo)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (planTratamiento == null) return NotFound();

            return View(planTratamiento);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var planTratamiento = await _context.PlanesDeTratamiento
                .Include(pt => pt.PasosDelPlan)
                .FirstOrDefaultAsync(pt => pt.Id == id);

            if (planTratamiento != null)
            {
                _context.PasosDeTratamiento.RemoveRange(planTratamiento.PasosDelPlan);
                _context.PlanesDeTratamiento.Remove(planTratamiento);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlanTratamientoExists(int id)
        {
            return _context.PlanesDeTratamiento.Any(e => e.Id == id);
        }

        // NUEVO: Exportar PDF usando Rotativa
        public async Task<IActionResult> ExportarPdf(int? id)
        {
            if (id == null) return NotFound();

            var plan = await _context.PlanesDeTratamiento
                .Include(p => p.Paciente)
                .Include(p => p.Odontologo)
                .Include(p => p.PasosDelPlan)
                    .ThenInclude(ps => ps.Tratamiento)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (plan == null) return NotFound();

            return new ViewAsPdf("ExportarPdf", plan)
            {
                FileName = $"PlanTratamiento_{plan.Paciente.Nombre.Replace(" ", "")}_{plan.FechaCreacion:yyyyMMdd}.pdf",
                PageSize = Rotativa.AspNetCore.Options.Size.A4
            };
        }
    }
}
