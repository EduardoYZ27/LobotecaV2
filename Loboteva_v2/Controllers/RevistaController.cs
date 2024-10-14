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
    public class RevistaController : Controller
    {
        private readonly LobotecaContext _context;

        public RevistaController(LobotecaContext context)
        {
            _context = context;
        }

        // GET: Revista
        public async Task<IActionResult> Index()
        {
            var lobotecaContext = _context.Revista.Include(r => r.IdEditorialNavigation).Include(r => r.IdCarreraNavigation);
            return View(await lobotecaContext.ToListAsync());
        }

        // GET: Revista/Create
        public IActionResult Create()
        {
            ViewData["IdEditorial"] = new SelectList(_context.Editorials, "Id", "Nombre");
            ViewData["IdCarrera"] = new SelectList(_context.Carreras, "Id", "NombreDeLaCarrera"); // Lista de carreras
            return View();
        }

        // POST: Revista/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Issn,FechaDePublicacion,Genero,Estado,RutaImagen,Archivo,FechaDeAlta,IdEditorial,IdCarrera")] Revistum revistum, IFormFile RutaImagen, IFormFile Archivo)
        {
            if (ModelState.IsValid)
            {
                // Guardar Imagen
                if (RutaImagen != null && RutaImagen.Length > 0)
                {
                    var fileNameImagen = Path.GetFileName(RutaImagen.FileName);
                    var filePathImagen = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileNameImagen);
                    using (var stream = new FileStream(filePathImagen, FileMode.Create))
                    {
                        await RutaImagen.CopyToAsync(stream);
                    }
                    revistum.RutaImagen = $"/images/{fileNameImagen}";
                }

                // Guardar Archivo PDF
                if (Archivo != null && Archivo.Length > 0)
                {
                    var fileNameArchivo = Path.GetFileName(Archivo.FileName);
                    var filePathArchivo = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/pdfs", fileNameArchivo);
                    using (var stream = new FileStream(filePathArchivo, FileMode.Create))
                    {
                        await Archivo.CopyToAsync(stream);
                    }
                    revistum.Archivo = $"/pdfs/{fileNameArchivo}";
                }

                _context.Add(revistum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEditorial"] = new SelectList(_context.Editorials, "Id", "Nombre", revistum.IdEditorial);
            ViewData["IdCarrera"] = new SelectList(_context.Carreras, "Id", "NombreDeLaCarrera", revistum.IdCarrera); // Mantener la selección de carrera
            return View(revistum);
        }

        // GET: Revista/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Revista == null)
            {
                return NotFound();
            }

            var revistum = await _context.Revista
                .Include(r => r.IdEditorialNavigation)
                .Include(r => r.IdCarreraNavigation) // Incluyendo Carrera
                .FirstOrDefaultAsync(m => m.Id == id);
            if (revistum == null)
            {
                return NotFound();
            }

            return View(revistum);
        }

        // GET: Revista/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Revista == null)
            {
                return NotFound();
            }

            var revistum = await _context.Revista.FindAsync(id);
            if (revistum == null)
            {
                return NotFound();
            }
            ViewData["IdEditorial"] = new SelectList(_context.Editorials, "Id", "Nombre", revistum.IdEditorial);
            ViewData["IdCarrera"] = new SelectList(_context.Carreras, "Id", "NombreDeLaCarrera", revistum.IdCarrera); // Lista de carreras
            return View(revistum);
        }

        // POST: Revista/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Issn,FechaDePublicacion,Genero,Estado,RutaImagen,Archivo,FechaDeAlta,IdEditorial,IdCarrera")] Revistum revistum, IFormFile RutaImagen, IFormFile Archivo)
        {
            if (id != revistum.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Guardar Imagen si hay un cambio
                    if (RutaImagen != null && RutaImagen.Length > 0)
                    {
                        var fileNameImagen = Path.GetFileName(RutaImagen.FileName);
                        var filePathImagen = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileNameImagen);
                        using (var stream = new FileStream(filePathImagen, FileMode.Create))
                        {
                            await RutaImagen.CopyToAsync(stream);
                        }
                        revistum.RutaImagen = $"/images/{fileNameImagen}";
                    }

                    // Guardar Archivo PDF si hay un cambio
                    if (Archivo != null && Archivo.Length > 0)
                    {
                        var fileNameArchivo = Path.GetFileName(Archivo.FileName);
                        var filePathArchivo = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/pdfs", fileNameArchivo);
                        using (var stream = new FileStream(filePathArchivo, FileMode.Create))
                        {
                            await Archivo.CopyToAsync(stream);
                        }
                        revistum.Archivo = $"/pdfs/{fileNameArchivo}";
                    }

                    _context.Update(revistum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RevistumExists(revistum.Id))
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
            ViewData["IdEditorial"] = new SelectList(_context.Editorials, "Id", "Nombre", revistum.IdEditorial);
            ViewData["IdCarrera"] = new SelectList(_context.Carreras, "Id", "NombreDeLaCarrera", revistum.IdCarrera); // Mantener la selección de carrera
            return View(revistum);
        }

        // GET: Revista/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Revista == null)
            {
                return NotFound();
            }

            var revistum = await _context.Revista
                .Include(r => r.IdEditorialNavigation)
                .Include(r => r.IdCarreraNavigation) // Incluyendo Carrera
                .FirstOrDefaultAsync(m => m.Id == id);
            if (revistum == null)
            {
                return NotFound();
            }

            return View(revistum);
        }

        // POST: Revista/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Revista == null)
            {
                return Problem("Entity set 'LobotecaContext.Revista' is null.");
            }
            var revistum = await _context.Revista.FindAsync(id);
            if (revistum != null)
            {
                _context.Revista.Remove(revistum);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool RevistumExists(int id)
        {
            return (_context.Revista?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
