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
    public class PacientesController : Controller
    {
        private readonly AppDbContext _context;

        public PacientesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Pacientes
        public async Task<IActionResult> Index()
        {
            // Carga todos los pacientes. Podríamos añadir paginación o búsqueda aquí.
            return View(await _context.Pacientes.ToListAsync());
        }

        // GET: Pacientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Carga el paciente y sus datos relacionados de forma correcta:
            // Incluye los planes de tratamiento del paciente
            // Para cada plan, incluye el odontólogo que lo creó
            // Para cada plan, también incluye sus pasos individuales
            // Y para cada paso, incluye el tipo de tratamiento asociado.
            // Finalmente, incluye los turnos del paciente y el odontólogo de cada turno.
            var paciente = await _context.Pacientes
                .Include(p => p.PlanesDeTratamiento)
                    .ThenInclude(pt => pt.Odontologo)
                .Include(p => p.PlanesDeTratamiento) // Se debe volver a incluir para una nueva ruta de ThenInclude
                    .ThenInclude(pt => pt.PasosDelPlan)
                        .ThenInclude(ps => ps.Tratamiento)
                .Include(p => p.Turnos)
                    .ThenInclude(t => t.Odontologo)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        // GET: Pacientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pacientes/Create
        // Para protegerse de ataques de overposting, habilite las propiedades específicas a las que quiere enlazarse. Para obtener
        // más detalles, vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,RUT,Telefono,Email,Direccion")] Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                // Verifica si ya existe un paciente con el mismo RUT (aunque el HasIndex ya lo maneja en DB, esto da un mensaje más amigable)
                if (await _context.Pacientes.AnyAsync(p => p.RUT == paciente.RUT))
                {
                    ModelState.AddModelError("RUT", "Ya existe un paciente con este RUT.");
                    return View(paciente);
                }

                _context.Add(paciente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(paciente);
        }

        // GET: Pacientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
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

        // POST: Pacientes/Edit/5
        // Para protegerse de ataques de overposting, habilite las propiedades específicas a las que quiere enlazarse. Para obtener
        // más detalles, vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,RUT,Telefono,Email,Direccion")] Paciente paciente)
        {
            if (id != paciente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Verifica si otro paciente ya usa el RUT modificado
                    if (await _context.Pacientes.AnyAsync(p => p.RUT == paciente.RUT && p.Id != paciente.Id))
                    {
                        ModelState.AddModelError("RUT", "Ya existe otro paciente con este RUT.");
                        return View(paciente);
                    }

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
                return RedirectToAction(nameof(Index));
            }
            return View(paciente);
        }

        // GET: Pacientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Pacientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente != null)
            {
                // Si la configuración de cascada está en la DB, EF lo manejará.
                // Si no, y necesitas borrar dependientes, tendrías que cargarlos y eliminarlos aquí.
                // Por ejemplo, para eliminar planes y turnos asociados:
                // var planes = await _context.PlanesDeTratamiento.Where(pt => pt.IdPaciente == id).ToListAsync();
                // _context.PlanesDeTratamiento.RemoveRange(planes);
                // var turnos = await _context.Turnos.Where(t => t.IdPaciente == id).ToListAsync();
                // _context.Turnos.RemoveRange(turnos);

                _context.Pacientes.Remove(paciente);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PacienteExists(int id)
        {
            return _context.Pacientes.Any(e => e.Id == id);
        }
    }
}