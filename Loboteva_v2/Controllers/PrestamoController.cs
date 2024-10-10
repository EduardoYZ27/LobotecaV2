using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Loboteva_v2.Models;

namespace Loboteva_v2.Controllers
{
    public class PrestamoController : Controller
    {
        private readonly LobotecaContext _context;

        public PrestamoController(LobotecaContext context)
        {
            _context = context;
        }

        // GET: Prestamo
        public async Task<IActionResult> Index()
        {
            var lobotecaContext = _context.Prestamos
                .Include(p => p.IdAdministradorNavigation)
                .Include(p => p.IdLibroNavigation);
            return View(await lobotecaContext.ToListAsync());
        }

        // GET: Prestamo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Prestamos == null)
            {
                return NotFound();
            }

            var prestamo = await _context.Prestamos
                .Include(p => p.IdAdministradorNavigation)
                .Include(p => p.IdLibroNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prestamo == null)
            {
                return NotFound();
            }

            return View(prestamo);
        }

        // GET: Prestamo/Create
        public IActionResult Create()
        {
            ViewData["IdAdministrador"] = new SelectList(_context.Administradors, "Id", "Nombre");
            ViewData["IdLibro"] = new SelectList(_context.Libros, "Id", "Titulo");
            ViewData["Estado"] = new SelectList(new[] { "Disponible", "No disponible" });
            return View();
        }

        // POST: Prestamo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FechaDePrestamo,IdAdministrador,Estado,IdLibro")] Prestamo prestamo)
        {
            if (ModelState.IsValid)
            {
                // Calcular la fecha de devolución sumando 3 días a la fecha de préstamo
                prestamo.FechaDeDevolucion = prestamo.FechaDePrestamo?.AddDays(3);

                _context.Add(prestamo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAdministrador"] = new SelectList(_context.Administradors, "Id", "Nombre", prestamo.IdAdministrador);
            ViewData["IdLibro"] = new SelectList(_context.Libros, "Id", "Titulo", prestamo.IdLibro);
            ViewData["Estado"] = new SelectList(new[] { "Disponible", "No disponible" }, prestamo.Estado);
            return View(prestamo);
        }

        // GET: Prestamo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Prestamos == null)
            {
                return NotFound();
            }

            var prestamo = await _context.Prestamos.FindAsync(id);
            if (prestamo == null)
            {
                return NotFound();
            }
            ViewData["IdAdministrador"] = new SelectList(_context.Administradors, "Id", "Nombre", prestamo.IdAdministrador);
            ViewData["IdLibro"] = new SelectList(_context.Libros, "Id", "Titulo", prestamo.IdLibro);
            ViewData["Estado"] = new SelectList(new[] { "Disponible", "No disponible" }, prestamo.Estado);
            return View(prestamo);
        }

        // POST: Prestamo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FechaDePrestamo,IdAdministrador,Estado,IdLibro")] Prestamo prestamo)
        {
            if (id != prestamo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Calcular la fecha de devolución sumando 3 días a la fecha de préstamo
                    prestamo.FechaDeDevolucion = prestamo.FechaDePrestamo?.AddDays(3);

                    _context.Update(prestamo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrestamoExists(prestamo.Id))
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
            ViewData["IdAdministrador"] = new SelectList(_context.Administradors, "Id", "Nombre", prestamo.IdAdministrador);
            ViewData["IdLibro"] = new SelectList(_context.Libros, "Id", "Titulo", prestamo.IdLibro);
            ViewData["Estado"] = new SelectList(new[] { "Disponible", "No disponible" }, prestamo.Estado);
            return View(prestamo);
        }

        // GET: Prestamo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Prestamos == null)
            {
                return NotFound();
            }

            var prestamo = await _context.Prestamos
                .Include(p => p.IdAdministradorNavigation)
                .Include(p => p.IdLibroNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prestamo == null)
            {
                return NotFound();
            }

            return View(prestamo);
        }

        // POST: Prestamo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prestamo = await _context.Prestamos.FindAsync(id);
            if (prestamo != null)
            {
                _context.Prestamos.Remove(prestamo);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool PrestamoExists(int id)
        {
            return _context.Prestamos.Any(e => e.Id == id);
        }
    }
}
