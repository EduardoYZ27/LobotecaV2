using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Loboteva_v2.Models;

namespace Loboteva_v2.Controllers
{
    public class SancionesController : Controller
    {
        private readonly LobotecaContext _context;

        public SancionesController(LobotecaContext context)
        {
            _context = context;
        }

        // GET: Sanciones
        public async Task<IActionResult> Index()
        {
            var lobotecaContext = _context.Sanciones
                .Include(s => s.IdAlumnoNavigation)
                .Include(s => s.IdPenalizacionNavigation)
                .Include(s => s.IdPrestamoNavigation)
                .ThenInclude(p => p.IdLibroNavigation); // Include the related Libro data
            return View(await lobotecaContext.ToListAsync());
        }

        // GET: Sanciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sanciones == null)
            {
                return NotFound();
            }

            var sancione = await _context.Sanciones
                .Include(s => s.IdAlumnoNavigation)
                .Include(s => s.IdPenalizacionNavigation)
                .Include(s => s.IdPrestamoNavigation)
                .ThenInclude(p => p.IdLibroNavigation) // Include the related Libro data
                .FirstOrDefaultAsync(m => m.IdSancion == id);
            if (sancione == null)
            {
                return NotFound();
            }

            return View(sancione);
        }

        // GET: Sanciones/Create
        public IActionResult Create()
        {
            ViewData["IdAlumno"] = new SelectList(_context.Alumnos, "Id", "Nombre");
            ViewData["IdPenalizacion"] = new SelectList(_context.ConfiguracionPenalizaciones, "IdPenalizacion", "Monto");
            ViewData["IdPrestamo"] = new SelectList(_context.Prestamos.Include(p => p.IdLibroNavigation), "Id", "IdLibroNavigation.Titulo"); // Show Titulo
            ViewData["Estado"] = new SelectList(new[] { "Disponible", "No disponible" });
            return View();
        }

        // POST: Sanciones/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdSancion,Estado,IdAlumno,IdPrestamo,IdPenalizacion")] Sancione sancione)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sancione);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAlumno"] = new SelectList(_context.Alumnos, "Id", "Nombre", sancione.IdAlumno);
            ViewData["IdPenalizacion"] = new SelectList(_context.ConfiguracionPenalizaciones, "IdPenalizacion", "Monto", sancione.IdPenalizacion);
            ViewData["IdPrestamo"] = new SelectList(_context.Prestamos.Include(p => p.IdLibroNavigation), "Id", "IdLibroNavigation.Titulo", sancione.IdPrestamo); // Show Titulo
            ViewData["Estado"] = new SelectList(new[] { "Disponible", "No disponible" }, sancione.Estado);
            return View(sancione);
        }

        // GET: Sanciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sanciones == null)
            {
                return NotFound();
            }

            var sancione = await _context.Sanciones.FindAsync(id);
            if (sancione == null)
            {
                return NotFound();
            }
            ViewData["IdAlumno"] = new SelectList(_context.Alumnos, "Id", "Nombre", sancione.IdAlumno);
            ViewData["IdPenalizacion"] = new SelectList(_context.ConfiguracionPenalizaciones, "IdPenalizacion", "Monto", sancione.IdPenalizacion);
            ViewData["IdPrestamo"] = new SelectList(_context.Prestamos.Include(p => p.IdLibroNavigation), "Id", "IdLibroNavigation.Titulo", sancione.IdPrestamo); // Show Titulo
            ViewData["Estado"] = new SelectList(new[] { "Disponible", "No disponible" }, sancione.Estado);
            return View(sancione);
        }

        // POST: Sanciones/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdSancion,Estado,IdAlumno,IdPrestamo,IdPenalizacion")] Sancione sancione)
        {
            if (id != sancione.IdSancion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sancione);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SancioneExists(sancione.IdSancion))
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
            ViewData["IdAlumno"] = new SelectList(_context.Alumnos, "Id", "Nombre", sancione.IdAlumno);
            ViewData["IdPenalizacion"] = new SelectList(_context.ConfiguracionPenalizaciones, "IdPenalizacion", "Descripcion", sancione.IdPenalizacion);
            ViewData["IdPrestamo"] = new SelectList(_context.Prestamos.Include(p => p.IdLibroNavigation), "Id", "IdLibroNavigation.Titulo", sancione.IdPrestamo); // Show Titulo
            ViewData["Estado"] = new SelectList(new[] { "Disponible", "No disponible" }, sancione.Estado);
            return View(sancione);
        }

        // GET: Sanciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sanciones == null)
            {
                return NotFound();
            }

            var sancione = await _context.Sanciones
                .Include(s => s.IdAlumnoNavigation)
                .Include(s => s.IdPenalizacionNavigation)
                .Include(s => s.IdPrestamoNavigation)
                .ThenInclude(p => p.IdLibroNavigation) // Include the related Libro data
                .FirstOrDefaultAsync(m => m.IdSancion == id);
            if (sancione == null)
            {
                return NotFound();
            }

            return View(sancione);
        }

        // POST: Sanciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sancione = await _context.Sanciones.FindAsync(id);
            if (sancione != null)
            {
                _context.Sanciones.Remove(sancione);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool SancioneExists(int id)
        {
            return _context.Sanciones.Any(e => e.IdSancion == id);
        }
    }
}
