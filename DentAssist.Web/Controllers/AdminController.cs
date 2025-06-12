using Microsoft.AspNetCore.Mvc;
using DentAssist.Web.Models.Data;
using DentAssist.Web.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DentAssist.Web.Controllers
{
    // [Authorize(Roles = "Administrador")] // Descomentar cuando implementemos la autenticación
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin
        public IActionResult Index()
        {
            return View(); // Vista principal del panel de administración
        }

        // --- Lógica para Odontólogos ---

        // GET: Admin/ListOdontologos
        public async Task<IActionResult> ListOdontologos()
        {
            var odontologos = await _context.Odontologos.ToListAsync();
            return View(odontologos);
        }

        // GET: Admin/CreateOdontologo
        public IActionResult CreateOdontologo()
        {
            return View();
        }

        // POST: Admin/CreateOdontologo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOdontologo([Bind("Nombre,Matricula,Especialidad,Email")] Odontologo odontologo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(odontologo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ListOdontologos));
            }
            return View(odontologo);
        }

        // GET: Admin/EditOdontologo/5
        public async Task<IActionResult> EditOdontologo(int? id)
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

        // POST: Admin/EditOdontologo/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditOdontologo(int id, [Bind("Id,Nombre,Matricula,Especialidad,Email")] Odontologo odontologo)
        {
            if (id != odontologo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
                return RedirectToAction(nameof(ListOdontologos));
            }
            return View(odontologo);
        }

        // GET: Admin/DeleteOdontologo/5 (Muestra la confirmación)
        public async Task<IActionResult> DeleteOdontologo(int? id)
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

        // POST: Admin/DeleteOdontologo/5 (Realiza la eliminación)
        [HttpPost, ActionName("DeleteOdontologo")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteOdontologoConfirmed(int id)
        {
            var odontologo = await _context.Odontologos.FindAsync(id);
            if (odontologo != null)
            {
                _context.Odontologos.Remove(odontologo);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ListOdontologos));
        }

        private bool OdontologoExists(int id)
        {
            return _context.Odontologos.Any(e => e.Id == id);
        }

        // --- Lógica para Tratamientos ---

        // GET: Admin/ListTratamientos
        public async Task<IActionResult> ListTratamientos()
        {
            var tratamientos = await _context.Tratamientos.ToListAsync();
            return View(tratamientos);
        }

        // GET: Admin/CreateTratamiento
        public IActionResult CreateTratamiento()
        {
            return View();
        }

        // POST: Admin/CreateTratamiento
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTratamiento([Bind("Nombre,Descripcion,PrecioEstimado")] Tratamiento tratamiento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tratamiento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ListTratamientos));
            }
            return View(tratamiento);
        }

        // GET: Admin/EditTratamiento/5
        public async Task<IActionResult> EditTratamiento(int? id)
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

        // POST: Admin/EditTratamiento/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTratamiento(int id, [Bind("Id,Nombre,Descripcion,PrecioEstimado")] Tratamiento tratamiento)
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
                return RedirectToAction(nameof(ListTratamientos));
            }
            return View(tratamiento);
        }

        // GET: Admin/DeleteTratamiento/5
        public async Task<IActionResult> DeleteTratamiento(int? id)
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

        // POST: Admin/DeleteTratamiento/5
        [HttpPost, ActionName("DeleteTratamiento")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteTratamientoConfirmed(int id)
        {
            var tratamiento = await _context.Tratamientos.FindAsync(id);
            if (tratamiento != null)
            {
                _context.Tratamientos.Remove(tratamiento);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ListTratamientos));
        }

        private bool TratamientoExists(int id)
        {
            return _context.Tratamientos.Any(e => e.Id == id);
        }
    }
}
