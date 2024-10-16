﻿using System;
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
    public class ELibroController : Controller
    {
        private readonly LobotecaContext _context;

        public ELibroController(LobotecaContext context)
        {
            _context = context;
        }

        // GET: ELibro
        public async Task<IActionResult> Index()
        {
            var lobotecaContext = _context.ELibros.Include(e => e.IdEditorialNavigation).Include(e => e.IdCarreraNavigation);
            return View(await lobotecaContext.ToListAsync());
        }

        // GET: ELibro/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ELibros == null)
            {
                return NotFound();
            }

            var eLibro = await _context.ELibros
                .Include(e => e.IdEditorialNavigation)
                .Include(e => e.IdCarreraNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (eLibro == null)
            {
                return NotFound();
            }

            return View(eLibro);
        }

        // GET: ELibro/Create
        public IActionResult Create()
        {
            ViewData["IdEditorial"] = new SelectList(_context.Editorials, "Id", "Nombre");
            ViewData["IdCarrera"] = new SelectList(_context.Carreras, "Id", "NombreDeLaCarrera");
            return View();
        }

        // POST: ELibro/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Isbn,FechaDePublicacion,Genero,Estado,RutaImagen,Archivo,FechaDeAlta,IdEditorial,IdCarrera")] ELibro eLibro, IFormFile RutaImagen, IFormFile Archivo)
        {
            if (ModelState.IsValid)
            {
                // Guardar la imagen si existe
                if (RutaImagen != null && RutaImagen.Length > 0)
                {
                    var fileName = Path.GetFileName(RutaImagen.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await RutaImagen.CopyToAsync(stream);
                    }

                    eLibro.RutaImagen = $"/images/{fileName}";
                }

                // Guardar el archivo PDF si existe
                if (Archivo != null && Archivo.Length > 0)
                {
                    var fileName = Path.GetFileName(Archivo.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/elibros", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await Archivo.CopyToAsync(stream);
                    }

                    eLibro.Archivo = $"/elibros/{fileName}";
                }

                _context.Add(eLibro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEditorial"] = new SelectList(_context.Editorials, "Id", "Nombre", eLibro.IdEditorial);
            ViewData["IdCarrera"] = new SelectList(_context.Carreras, "Id", "NombreDeLaCarrera", eLibro.IdCarrera);
            return View(eLibro);
        }

        // GET: ELibro/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ELibros == null)
            {
                return NotFound();
            }

            var eLibro = await _context.ELibros.FindAsync(id);
            if (eLibro == null)
            {
                return NotFound();
            }
            ViewData["IdEditorial"] = new SelectList(_context.Editorials, "Id", "Nombre", eLibro.IdEditorial);
            ViewData["IdCarrera"] = new SelectList(_context.Carreras, "Id", "NombreDeLaCarrera", eLibro.IdCarrera);
            return View(eLibro);
        }

        // POST: ELibro/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Isbn,FechaDePublicacion,Genero,Estado,RutaImagen,Archivo,FechaDeAlta,IdEditorial,IdCarrera")] ELibro eLibro, IFormFile RutaImagen, IFormFile Archivo)
        {
            if (id != eLibro.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Guardar la imagen si existe
                    if (RutaImagen != null && RutaImagen.Length > 0)
                    {
                        var fileName = Path.GetFileName(RutaImagen.FileName);
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await RutaImagen.CopyToAsync(stream);
                        }

                        eLibro.RutaImagen = $"/images/{fileName}";
                    }

                    // Guardar el archivo PDF si existe
                    if (Archivo != null && Archivo.Length > 0)
                    {
                        var fileName = Path.GetFileName(Archivo.FileName);
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/elibros", fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await Archivo.CopyToAsync(stream);
                        }

                        eLibro.Archivo = $"/elibros/{fileName}";
                    }

                    _context.Update(eLibro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ELibroExists(eLibro.Id))
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
            ViewData["IdEditorial"] = new SelectList(_context.Editorials, "Id", "Nombre", eLibro.IdEditorial);
            ViewData["IdCarrera"] = new SelectList(_context.Carreras, "Id", "NombreDeLaCarrera", eLibro.IdCarrera);
            return View(eLibro);
        }

        // GET: ELibro/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ELibros == null)
            {
                return NotFound();
            }

            var eLibro = await _context.ELibros
                .Include(e => e.IdEditorialNavigation)
                .Include(e => e.IdCarreraNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eLibro == null)
            {
                return NotFound();
            }

            return View(eLibro);
        }

        // POST: ELibro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ELibros == null)
            {
                return Problem("Entity set 'LobotecaContext.ELibros' is null.");
            }
            var eLibro = await _context.ELibros.FindAsync(id);
            if (eLibro != null)
            {
                _context.ELibros.Remove(eLibro);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ELibroExists(int id)
        {
            return (_context.ELibros?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
