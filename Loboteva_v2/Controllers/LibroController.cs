using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Loboteva_v2.Models;

namespace Loboteva_v2.Controllers
{
    public class LibroController : Controller
    {
        private readonly LobotecaContext _context;

        public LibroController(LobotecaContext context)
        {
            _context = context;
        }

        // GET: Libro
        public async Task<IActionResult> Index()
        {
            var lobotecaContext = _context.Libros.Include(l => l.IdEditorialNavigation);
            return View(await lobotecaContext.ToListAsync());
        }

        // GET: Libro/Create
        public IActionResult Create()
        {
            ViewData["IdEditorial"] = new SelectList(_context.Editorials, "Id", "Nombre");
            return View();
        }

        // POST: Libro/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Isbn,FechaDePublicacion,Genero,Estado,RutaImagen,Stock,FechaDeAlta,IdEditorial")] Libro libro, IFormFile RutaImagen)
        {
            if (ModelState.IsValid)
            {
                if (RutaImagen != null && RutaImagen.Length > 0)
                {
                    // Generar un nombre único para el archivo
                    var fileName = Path.GetFileName(RutaImagen.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                    // Guardar el archivo en la carpeta local
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await RutaImagen.CopyToAsync(stream);
                    }

                    // Guardar la ruta relativa del archivo en la base de datos
                    libro.RutaImagen = $"/images/{fileName}";
                }

                _context.Add(libro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEditorial"] = new SelectList(_context.Editorials, "Id", "Nombre", libro.IdEditorial);
            return View(libro);
        }

        // GET: Libro/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Libros == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros
                .Include(l => l.IdEditorialNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        // GET: Libro/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Libros == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros.FindAsync(id);
            if (libro == null)
            {
                return NotFound();
            }
            ViewData["IdEditorial"] = new SelectList(_context.Editorials, "Id", "Nombre", libro.IdEditorial);
            return View(libro);
        }

        // POST: Libro/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Isbn,FechaDePublicacion,Genero,Estado,RutaImagen,Stock,FechaDeAlta,IdEditorial")] Libro libro, IFormFile RutaImagen)
        {
            if (id != libro.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (RutaImagen != null && RutaImagen.Length > 0)
                    {
                        var fileName = Path.GetFileName(RutaImagen.FileName);
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await RutaImagen.CopyToAsync(stream);
                        }

                        libro.RutaImagen = $"/images/{fileName}";
                    }

                    _context.Update(libro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LibroExists(libro.Id))
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
            ViewData["IdEditorial"] = new SelectList(_context.Editorials, "Id", "Nombre", libro.IdEditorial);
            return View(libro);
        }

        // GET: Libro/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Libros == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros
                .Include(l => l.IdEditorialNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        // POST: Libro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var libro = await _context.Libros.FindAsync(id);
            if (libro != null)
            {
                _context.Libros.Remove(libro);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool LibroExists(int id)
        {
            return (_context.Libros?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
