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
    public class PasoTratamientosController : Controller
    {
        private readonly AppDbContext _context;

        public PasoTratamientosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: PasoTratamientos (puede ser útil para una vista de todos los pasos, aunque lo principal será desde PlanTratamiento)
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.PasosDeTratamiento
                                 .Include(p => p.PlanTratamiento)
                                     .ThenInclude(pt => pt.Paciente) // Para ver el paciente del plan
                                 .Include(p => p.Tratamiento);
            return View(await appDbContext.ToListAsync());
        }

        // GET: PasoTratamientos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pasoTratamiento = await _context.PasosDeTratamiento
                .Include(p => p.PlanTratamiento)
                    .ThenInclude(pt => pt.Paciente)
                .Include(p => p.Tratamiento)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pasoTratamiento == null)
            {
                return NotFound();
            }

            return View(pasoTratamiento);
        }

        // GET: PasoTratamientos/Create (Normalmente se llamará desde la vista de PlanTratamiento/Edit)
        public IActionResult Create(int? idPlanTratamiento) // El parámetro se llama idPlanTratamiento
        {
            // Cargar la lista de tratamientos para el dropdown
            ViewData["IdTratamiento"] = new SelectList(_context.Tratamientos, "Id", "Nombre");

            // Crea una instancia del modelo, incluso si está vacía.
            // Esto evita NullReferenceExceptions en la vista cuando @Model es null.
            var model = new PasoTratamiento();

            // **CORRECCIÓN CLAVE 1:** Inicializar ViewBag.PlanTratamientoId siempre, incluso si es null.
            ViewBag.PlanTratamientoId = idPlanTratamiento; // Asigna el int? directamente

            if (idPlanTratamiento.HasValue)
            {
                // Si se pasó un planTratamientoId, asignarlo al modelo para el hidden field
                model.IdPlanTratamiento = idPlanTratamiento.Value;

                // Cargar información del Plan de Tratamiento para mostrar
                var plan = _context.PlanesDeTratamiento
                                   .Include(p => p.Paciente) // Para poder mostrar el nombre del paciente
                                   .FirstOrDefault(p => p.Id == idPlanTratamiento.Value);

                if (plan != null)
                {
                    ViewBag.PlanTratamientoInfo = $"Plan #{plan.Id} - Paciente: {plan.Paciente?.Nombre ?? "N/A"}";
                }
                else
                {
                    ViewBag.PlanTratamientoInfo = "Plan de Tratamiento no encontrado.";
                }
            }
            else
            {
                // Si no se pasó un idPlanTratamiento, mostrar el dropdown completo de planes
                ViewData["IdPlanTratamiento"] = new SelectList(_context.PlanesDeTratamiento.Include(pt => pt.Paciente), "Id", "Paciente.Nombre");
            }

            // Retorna la vista pasando el modelo.
            return View(model);
        }

        // POST: PasoTratamientos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdPlanTratamiento,IdTratamiento,Secuencia,FechaEstimada,FechaRealizado,Estado,ObservacionesClinicas")] PasoTratamiento pasoTratamiento)
        {
            if (ModelState.IsValid)
            {
                // Establecer estado por defecto si no se especifica
                if (string.IsNullOrEmpty(pasoTratamiento.Estado))
                {
                    pasoTratamiento.Estado = "Pendiente";
                }
                _context.Add(pasoTratamiento);
                await _context.SaveChangesAsync();
                // Redirigir de vuelta al Plan de Tratamiento
                return RedirectToAction("Edit", "PlanTratamientos", new { id = pasoTratamiento.IdPlanTratamiento });
            }

            // Si el ModelState no es válido, recargar los ViewData y ViewBag para que el formulario se muestre correctamente
            ViewData["IdTratamiento"] = new SelectList(_context.Tratamientos, "Id", "Nombre", pasoTratamiento.IdTratamiento);

            // Recargar ViewBag.PlanTratamientoId y PlanTratamientoInfo en caso de ModelState inválido
            ViewBag.PlanTratamientoId = pasoTratamiento.IdPlanTratamiento; // Asigna el ID del modelo
            if (pasoTratamiento.IdPlanTratamiento != 0) // Asumiendo que 0 es un ID inválido para un plan
            {
                var plan = _context.PlanesDeTratamiento
                                   .Include(p => p.Paciente)
                                   .FirstOrDefault(p => p.Id == pasoTratamiento.IdPlanTratamiento);
                ViewBag.PlanTratamientoInfo = plan != null ? $"Plan #{plan.Id} - Paciente: {plan.Paciente?.Nombre ?? "N/A"}" : "Información no disponible";
            }
            else
            {
                ViewData["IdPlanTratamiento"] = new SelectList(_context.PlanesDeTratamiento.Include(pt => pt.Paciente), "Id", "Paciente.Nombre", pasoTratamiento.IdPlanTratamiento);
            }

            return View(pasoTratamiento);
        }

        // GET: PasoTratamientos/Edit/5 (Puede usarse para editar un paso individualmente)
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // **CORRECCIÓN CLAVE:** Usar .Include() y .ThenInclude() para cargar las propiedades de navegación
            var pasoTratamiento = await _context.PasosDeTratamiento
                .Include(p => p.PlanTratamiento)          // Incluye el PlanTratamiento
                    .ThenInclude(pt => pt.Paciente)       // Y dentro del PlanTratamiento, incluye el Paciente
                .Include(p => p.Tratamiento)              // Incluye el Tratamiento
                .FirstOrDefaultAsync(m => m.Id == id);    // Busca el PasoTratamiento por su ID

            if (pasoTratamiento == null)
            {
                return NotFound();
            }

            // Asegurarse de que los SelectList estén bien cargados
            ViewData["IdPlanTratamiento"] = new SelectList(_context.PlanesDeTratamiento.Include(pt => pt.Paciente), "Id", "Paciente.Nombre", pasoTratamiento.IdPlanTratamiento);
            ViewData["IdTratamiento"] = new SelectList(_context.Tratamientos, "Id", "Nombre", pasoTratamiento.IdTratamiento);

            return View(pasoTratamiento);
        }

        // POST: PasoTratamientos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdPlanTratamiento,IdTratamiento,Secuencia,FechaEstimada,FechaRealizado,Estado,ObservacionesClinicas")] PasoTratamiento pasoTratamiento)
        {
            if (id != pasoTratamiento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Si el estado es "Realizado", establecer FechaRealizado a la actual si no está ya
                    if (pasoTratamiento.Estado == "Realizado" && !pasoTratamiento.FechaRealizado.HasValue)
                    {
                        pasoTratamiento.FechaRealizado = DateTime.Now;
                    }
                    else if (pasoTratamiento.Estado != "Realizado")
                    {
                        pasoTratamiento.FechaRealizado = null; // Limpiar si el estado cambia
                    }

                    _context.Update(pasoTratamiento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PasoTratamientoExists(pasoTratamiento.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Edit", "PlanTratamientos", new { id = pasoTratamiento.IdPlanTratamiento }); // Volver al plan
            }
            ViewData["IdPlanTratamiento"] = new SelectList(_context.PlanesDeTratamiento.Include(pt => pt.Paciente), "Id", "Paciente.Nombre", pasoTratamiento.IdPlanTratamiento);
            ViewData["IdTratamiento"] = new SelectList(_context.Tratamientos, "Id", "Nombre", pasoTratamiento.IdTratamiento);
            return View(pasoTratamiento);
        }

        // GET: PasoTratamientos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pasoTratamiento = await _context.PasosDeTratamiento
                .Include(p => p.PlanTratamiento)
                    .ThenInclude(pt => pt.Paciente)
                .Include(p => p.Tratamiento)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pasoTratamiento == null)
            {
                return NotFound();
            }

            return View(pasoTratamiento);
        }

        // POST: PasoTratamientos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pasoTratamiento = await _context.PasosDeTratamiento.FindAsync(id);
            if (pasoTratamiento != null)
            {
                _context.PasosDeTratamiento.Remove(pasoTratamiento);
            }

            await _context.SaveChangesAsync();
            // Redirigir de vuelta al Plan de Tratamiento
            if (pasoTratamiento != null)
            {
                return RedirectToAction("Edit", "PlanTratamientos", new { id = pasoTratamiento.IdPlanTratamiento });
            }
            return RedirectToAction(nameof(Index)); // Si no hay plan, volver al índice general
        }

        private bool PasoTratamientoExists(int id)
        {
            return _context.PasosDeTratamiento.Any(e => e.Id == id);
        }
    }
}