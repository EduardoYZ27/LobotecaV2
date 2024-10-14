using Microsoft.AspNetCore.Mvc;
using Loboteca_v2.Models;
using System.Linq;
using Loboteva_v2.Models;

namespace Loboteca1.Controllers
{
    public class IndustriController : Controller
    {
        private readonly LobotecaContext _context; // Usamos LobotecaContext en lugar de ApplicationDbContext

        public IndustriController(LobotecaContext context)
        {
            _context = context;
        }

        public IActionResult Industri()
        {
            // Filtramos los 6 libros más recientes que pertenecen a la carrera de Industrial
            var librosRecientes = _context.ELibros
                .Where(l => l.IdCarrera == 4) // Suponiendo que 1 es el ID de Industrial
                .OrderByDescending(l => l.Id)
                .Take(6)
                .ToList();

            // Obtenemos las 6 revistas más recientes sin filtrar por carrera
            var revistasRecientes = _context.Revista
                .OrderByDescending(r => r.Id)
                .Take(6)
                .ToList();

            // Pasamos ambos conjuntos de datos a la vista
            ViewBag.LibrosRecientes = librosRecientes;
            ViewBag.RevistasRecientes = revistasRecientes;

            return View();
        }
    }
}
