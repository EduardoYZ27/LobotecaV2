using Microsoft.AspNetCore.Mvc;
using Loboteca_v2.Models;
using System.Linq;
using Loboteva_v2.Models;

namespace Loboteca1.Controllers
{
    public class MecatronicaController : Controller
    {
        private readonly LobotecaContext _context;

        public MecatronicaController(LobotecaContext context)
        {
            _context = context;
        }

        public IActionResult Mecatronica()
        {
            // Suponiendo que el ID de la carrera de Mecatrónica es 7
            var idCarreraMecatronica = 7;

            // Obtener los 6 libros más recientes para la carrera de Mecatrónica
            var librosRecientes = _context.ELibros
                .Where(l => l.IdCarrera == idCarreraMecatronica)
                .OrderByDescending(l => l.Id)
                .Take(6)
                .ToList();

            // Obtener las 6 revistas más recientes para la carrera de Mecatrónica
            var revistasRecientes = _context.Revista
                .Where(r => r.IdCarrera == idCarreraMecatronica)
                .OrderByDescending(r => r.Id)
                .Take(6)
                .ToList();

            // Pasar los libros y revistas a la vista usando ViewBag
            ViewBag.LibrosRecientes = librosRecientes;
            ViewBag.RevistasRecientes = revistasRecientes;

            return View();
        }
    }
}
