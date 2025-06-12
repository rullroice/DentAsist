using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using DentAssist.Web.Models; // Asume que tienes un modelo de error simple

namespace DentAssist.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // Constructor para inyectar el logger
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Acci�n para la p�gina de inicio
        public IActionResult Index()
        {
            return View(); // Retorna la vista Index.cshtml
        }

        // Acci�n para la p�gina de privacidad (ejemplo)
        public IActionResult Privacy()
        {
            return View(); // Retorna la vista Privacy.cshtml
        }

        // Acci�n para manejar errores
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}