using Microsoft.AspNetCore.Mvc;
using DentAssist.Web.Models.Data;
using DentAssist.Web.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering; // Para SelectList

namespace DentAssist.Web.Controllers
{
    // [Authorize(Roles = "Recepcionista")] // Descomentar cuando implementemos la autenticación
    public class RecepcionistaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RecepcionistaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Recepcionista
        public IActionResult Index()
        {
            return View(); // Vista principal de la recepcionista
        }

        // --- Lógica para Pacientes ---

        // GET: Recepcionista/ListPacientes
        public async Task<IActionResult> ListPacientes()
        {
            var pacientes = await _context.Pacientes.ToListAsync();
            return View(pacientes);
        }

        // GET: Recepcionista/CreatePaciente
        public IActionResult CreatePaciente()
        {
            return View();
        }

        // POST: Recepcionista/CreatePaciente
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePaciente([Bind("Nombre,RUT,Telefono,Email,Direccion")] Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paciente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ListPacientes));
            }
            return View(paciente);
        }

        // GET: Recepcionista/EditPaciente/5
        public async Task<IActionResult> EditPaciente(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente == null)
            {
                return NotFound();
            }
            return View(paciente);
        }

        // POST: Recepcionista/EditPaciente/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPaciente(int id, [Bind("Id,Nombre,RUT,Telefono,Email,Direccion")] Paciente paciente)
        {
            if (id != paciente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paciente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PacienteExists(paciente.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(ListPacientes));
            }
            return View(paciente);
        }

        // GET: Recepcionista/DeletePaciente/5
        public async Task<IActionResult> DeletePaciente(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _context.Pacientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        // POST: Recepcionista/DeletePaciente/5
        [HttpPost, ActionName("DeletePaciente")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePacienteConfirmed(int id)
        {
            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente != null)
            {
                _context.Pacientes.Remove(paciente);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ListPacientes));
        }

        private bool PacienteExists(int id)
        {
            return _context.Pacientes.Any(e => e.Id == id);
        }

        // --- Lógica para Gestión de Turnos ---

        // GET: Recepcionista/ListTurnos
        public async Task<IActionResult> ListTurnos()
        {
            // Incluir el paciente y el odontólogo asociado al turno para mostrarlos
            var turnos = await _context.Turnos
                                    .Include(t => t.Paciente)
                                    .Include(t => t.Odontologo)
                                    .OrderBy(t => t.FechaHora)
                                    .ToListAsync();
            return View(turnos);
        }

        // GET: Recepcionista/CreateTurno
        public async Task<IActionResult> CreateTurno()
        {
            // Necesitamos listas de pacientes y odontólogos para los dropdowns en la vista
            ViewData["PacienteId"] = new SelectList(await _context.Pacientes.ToListAsync(), "Id", "Nombre");
            ViewData["OdontologoId"] = new SelectList(await _context.Odontologos.ToListAsync(), "Id", "Nombre");
            return View();
        }

        // POST: Recepcionista/CreateTurno
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTurno([Bind("FechaHora,DuracionMinutos,PacienteId,OdontologoId,Estado")] Turno turno)
        {
            if (ModelState.IsValid)
            {
                _context.Add(turno);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ListTurnos));
            }
            // Si hay errores, recargar los SelectList antes de volver a la vista
            ViewData["PacienteId"] = new SelectList(await _context.Pacientes.ToListAsync(), "Id", "Nombre", turno.PacienteId);
            ViewData["OdontologoId"] = new SelectList(await _context.Odontologos.ToListAsync(), "Id", "Nombre", turno.OdontologoId);
            return View(turno);
        }

        // GET: Recepcionista/EditTurno/5
        public async Task<IActionResult> EditTurno(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turno = await _context.Turnos.FindAsync(id);
            if (turno == null)
            {
                return NotFound();
            }

            ViewData["PacienteId"] = new SelectList(await _context.Pacientes.ToListAsync(), "Id", "Nombre", turno.PacienteId);
            ViewData["OdontologoId"] = new SelectList(await _context.Odontologos.ToListAsync(), "Id", "Nombre", turno.OdontologoId);
            return View(turno);
        }

        // POST: Recepcionista/EditTurno/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTurno(int id, [Bind("Id,FechaHora,DuracionMinutos,PacienteId,OdontologoId,Estado")] Turno turno)
        {
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
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TurnoExists(turno.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(ListTurnos));
            }
            ViewData["PacienteId"] = new SelectList(await _context.Pacientes.ToListAsync(), "Id", "Nombre", turno.PacienteId);
            ViewData["OdontologoId"] = new SelectList(await _context.Odontologos.ToListAsync(), "Id", "Nombre", turno.OdontologoId);
            return View(turno);
        }

        // GET: Recepcionista/DeleteTurno/5
        public async Task<IActionResult> DeleteTurno(int? id)
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

        // POST: Recepcionista/DeleteTurno/5
        [HttpPost, ActionName("DeleteTurno")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteTurnoConfirmed(int id)
        {
            var turno = await _context.Turnos.FindAsync(id);
            if (turno != null)
            {
                _context.Turnos.Remove(turno);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ListTurnos));
        }

        private bool TurnoExists(int id)
        {
            return _context.Turnos.Any(e => e.Id == id);
        }
    }
}