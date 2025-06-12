using Microsoft.AspNetCore.Mvc;
using DentAssist.Web.Models.Data;
using DentAssist.Web.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering; // Para SelectList

namespace DentAssist.Web.Controllers
{
    // [Authorize(Roles = "Odontologo")] // Descomentar cuando implementemos la autenticación
    public class OdontologoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OdontologoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Odontologo
        public IActionResult Index()
        {
            // En un escenario real, aquí se obtendría el ID del odontólogo logeado
            // Por ahora, asumiremos un odontólogo de prueba o pasaremos el ID para demostración
            return View(); // Vista principal del odontólogo
        }

        // GET: Odontologo/MyAgenda
        // Muestra la agenda semanal del odontólogo
        public async Task<IActionResult> MyAgenda(int odontologoId) // En un sistema real, el odontologoId se obtendría del usuario autenticado
        {
            // Obtener el odontólogo (para mostrar su nombre, etc.)
            var odontologo = await _context.Odontologos.FindAsync(odontologoId);
            if (odontologo == null)
            {
                return NotFound("Odontólogo no encontrado.");
            }

            // Obtener turnos para el odontólogo actual y la semana actual
            // Para simplificar, obtenemos los turnos de la próxima semana a partir de hoy
            var startOfWeek = DateTime.Today; // O puedes calcular el inicio de la semana actual
            var endOfWeek = startOfWeek.AddDays(7); // La próxima semana

            var turnos = await _context.Turnos
                                    .Include(t => t.Paciente) // Para mostrar el nombre del paciente
                                    .Where(t => t.OdontologoId == odontologoId &&
                                                t.FechaHora >= startOfWeek &&
                                                t.FechaHora <= endOfWeek)
                                    .OrderBy(t => t.FechaHora)
                                    .ToListAsync();

            ViewBag.OdontologoNombre = odontologo.Nombre;
            ViewBag.AgendaPeriodo = $"{startOfWeek.ToShortDateString()} - {endOfWeek.ToShortDateString()}";
            return View(turnos);
        }

        // GET: Odontologo/ListPacientesHistorial (Lista pacientes para seleccionar y ver historial)
        public async Task<IActionResult> ListPacientesHistorial()
        {
            var pacientes = await _context.Pacientes.ToListAsync();
            return View(pacientes);
        }

        // GET: Odontologo/ViewHistorialClinico/5
        // Accede al historial clínico de un paciente
        public async Task<IActionResult> ViewHistorialClinico(int? pacienteId)
        {
            if (pacienteId == null)
            {
                return NotFound();
            }

            var paciente = await _context.Pacientes
                                        .Include(p => p.PlanesTratamiento)
                                            .ThenInclude(pt => pt.Detalles)
                                                .ThenInclude(dpt => dpt.Tratamiento) // Incluir los detalles y el tipo de tratamiento
                                        .FirstOrDefaultAsync(p => p.Id == pacienteId);

            if (paciente == null)
            {
                return NotFound("Paciente no encontrado.");
            }
            return View(paciente);
        }


        // --- Lógica para Plan de Tratamiento (Funcionalidad clave) ---

        // GET: Odontologo/ListPlanesTratamiento (Lista todos los planes de tratamiento para un odontólogo)
        public async Task<IActionResult> ListPlanesTratamiento(int odontologoId) // El ID del odontólogo se obtendría del usuario autenticado
        {
            var planes = await _context.PlanesTratamiento
                                    .Include(pt => pt.Paciente)
                                    .Include(pt => pt.Odontologo)
                                    .Where(pt => pt.OdontologoId == odontologoId)
                                    .OrderByDescending(pt => pt.FechaCreacion)
                                    .ToListAsync();
            return View(planes);
        }


        // GET: Odontologo/CreatePlanTratamiento (Muestra el formulario para crear un nuevo plan)
        public async Task<IActionResult> CreatePlanTratamiento(int? pacienteId)
        {
            // Necesitamos seleccionar un paciente si no se especifica
            ViewData["PacienteId"] = new SelectList(await _context.Pacientes.ToListAsync(), "Id", "Nombre", pacienteId);
            ViewData["Tratamientos"] = new SelectList(await _context.Tratamientos.ToListAsync(), "Id", "Nombre"); // Para los pasos del plan
            return View();
        }

        // POST: Odontologo/CreatePlanTratamiento
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePlanTratamiento(
            [Bind("PacienteId,OdontologoId,ObservacionesGenerales")] PlanTratamiento planTratamiento,
            [FromForm] List<int> tratamientoIds,
            [FromForm] List<DateTime?> fechasEstimadas,
            [FromForm] List<string> observacionesDetalle)
        {
            // OdontologoId debería ser del usuario logeado, aquí lo asignamos para la prueba
            // planTratamiento.OdontologoId = <ID_DEL_ODONTOLOGO_AUTENTICADO>;

            // Validar que se asignó un PacienteId válido
            if (planTratamiento.PacienteId == 0)
            {
                ModelState.AddModelError("PacienteId", "Debe seleccionar un paciente.");
            }

            if (ModelState.IsValid)
            {
                planTratamiento.FechaCreacion = DateTime.Now;
                _context.Add(planTratamiento);
                await _context.SaveChangesAsync(); // Guardar el plan principal para obtener su ID

                // Ahora agregar los detalles del plan
                if (tratamientoIds != null && tratamientoIds.Any())
                {
                    for (int i = 0; i < tratamientoIds.Count; i++)
                    {
                        var detalle = new DetallePlanTratamiento
                        {
                            PlanTratamientoId = planTratamiento.Id,
                            TratamientoId = tratamientoIds[i],
                            FechaEstimada = fechasEstimadas.Count > i ? fechasEstimadas[i] : null,
                            ObservacionesClinicas = observacionesDetalle.Count > i ? observacionesDetalle[i] : null,
                            Estado = DetallePlanEstado.Pendiente // Estado inicial
                        };
                        _context.Add(detalle);
                    }
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(ViewPlanTratamiento), new { id = planTratamiento.Id });
            }

            // Recargar datos para SelectLists si el modelo no es válido
            ViewData["PacienteId"] = new SelectList(await _context.Pacientes.ToListAsync(), "Id", "Nombre", planTratamiento.PacienteId);
            ViewData["Tratamientos"] = new SelectList(await _context.Tratamientos.ToListAsync(), "Id", "Nombre");
            return View(planTratamiento);
        }

        // GET: Odontologo/ViewPlanTratamiento/5
        // Permite visualizar el avance general del plan y detalles
        public async Task<IActionResult> ViewPlanTratamiento(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planTratamiento = await _context.PlanesTratamiento
                                            .Include(pt => pt.Paciente)
                                            .Include(pt => pt.Odontologo)
                                            .Include(pt => pt.Detalles)
                                                .ThenInclude(dpt => dpt.Tratamiento)
                                            .FirstOrDefaultAsync(m => m.Id == id);
            if (planTratamiento == null)
            {
                return NotFound("Plan de Tratamiento no encontrado.");
            }
            return View(planTratamiento);
        }

        // GET: Odontologo/EditDetallePlan/5 (Para editar un paso específico del plan)
        public async Task<IActionResult> EditDetallePlan(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalle = await _context.DetallesPlanTratamiento
                                        .Include(d => d.Tratamiento)
                                        .FirstOrDefaultAsync(d => d.Id == id);
            if (detalle == null)
            {
                return NotFound();
            }

            // Necesitamos saber a qué plan pertenece este detalle para la redirección
            ViewBag.PlanTratamientoId = detalle.PlanTratamientoId;
            return View(detalle);
        }

        // POST: Odontologo/EditDetallePlan/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDetallePlan(int id, [Bind("Id,PlanTratamientoId,TratamientoId,FechaEstimada,ObservacionesClinicas,Estado")] DetallePlanTratamiento detalle)
        {
            if (id != detalle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detalle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetallePlanTratamientoExists(detalle.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                // Redirigir de vuelta al plan de tratamiento principal
                return RedirectToAction(nameof(ViewPlanTratamiento), new { id = detalle.PlanTratamientoId });
            }
            // Si hay errores, volver a mostrar el formulario
            ViewBag.PlanTratamientoId = detalle.PlanTratamientoId;
            return View(detalle);
        }

        private bool DetallePlanTratamientoExists(int id)
        {
            return _context.DetallesPlanTratamiento.Any(e => e.Id == id);
        }

        // Acción para exportar el plan a PDF (Esto requerirá una librería de PDF, e.g., iTextSharp, Rotativa)
        // Por ahora, solo es un placeholder.
        public IActionResult ExportPlanToPdf(int id)
        {
            // Lógica para generar el PDF. Esto es complejo y requerirá una librería externa.
            // Por ejemplo:
            // var plan = _context.PlanesTratamiento
            //                  .Include(pt => pt.Paciente)
            //                  .Include(pt => pt.Odontologo)
            //                  .Include(pt => pt.Detalles)
            //                      .ThenInclude(dpt => dpt.Tratamiento)
            //                  .FirstOrDefault(m => m.Id == id);
            // if (plan == null) return NotFound();
            //
            // // Aquí iría la lógica para construir el PDF
            // byte[] pdfBytes = ...; // Lógica para generar PDF
            // return File(pdfBytes, "application/pdf", $"PlanDeTratamiento_{plan.Paciente.Nombre}_{plan.Id}.pdf");

            return Content($"Funcionalidad de exportación a PDF para el Plan de Tratamiento {id} (requiere implementación con librería externa).");
        }
    }
}
