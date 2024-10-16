﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Loboteva_v2.Models;

namespace Loboteva_v2.Controllers
{
    public class AutorELibroController : Controller
    {
        private readonly LobotecaContext _context;

        public AutorELibroController(LobotecaContext context)
        {
            _context = context;
        }

        // GET: AutorELibro
        public async Task<IActionResult> Index()
        {
            var lobotecaContext = _context.AutorELibros
                .Include(a => a.IdAutorNavigation)
                .Include(a => a.IdELibroNavigation);
            return View(await lobotecaContext.ToListAsync());
        }

        // GET: AutorELibro/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AutorELibros == null)
            {
                return NotFound();
            }

            var autorELibro = await _context.AutorELibros
                .Include(a => a.IdAutorNavigation)
                .Include(a => a.IdELibroNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (autorELibro == null)
            {
                return NotFound();
            }

            return View(autorELibro);
        }

        // GET: AutorELibro/Create
        public IActionResult Create()
        {
            ViewData["IdAutor"] = new SelectList(_context.Autors, "Id", "Nombre");
            ViewData["IdELibro"] = new SelectList(_context.ELibros, "Id", "Titulo");
            return View();
        }

        // POST: AutorELibro/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdAutor,IdELibro")] AutorELibro autorELibro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(autorELibro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAutor"] = new SelectList(_context.Autors, "Id", "Nombre", autorELibro.IdAutor);
            ViewData["IdELibro"] = new SelectList(_context.ELibros, "Id", "Titulo", autorELibro.IdELibro);
            return View(autorELibro);
        }

        // GET: AutorELibro/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AutorELibros == null)
            {
                return NotFound();
            }

            var autorELibro = await _context.AutorELibros.FindAsync(id);
            if (autorELibro == null)
            {
                return NotFound();
            }
            ViewData["IdAutor"] = new SelectList(_context.Autors, "Id", "Nombre", autorELibro.IdAutor);
            ViewData["IdELibro"] = new SelectList(_context.ELibros, "Id", "Titulo", autorELibro.IdELibro);
            return View(autorELibro);
        }

        // POST: AutorELibro/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdAutor,IdELibro")] AutorELibro autorELibro)
        {
            if (id != autorELibro.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(autorELibro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AutorELibroExists(autorELibro.Id))
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
            ViewData["IdAutor"] = new SelectList(_context.Autors, "Id", "Nombre", autorELibro.IdAutor);
            ViewData["IdELibro"] = new SelectList(_context.ELibros, "Id", "Titulo", autorELibro.IdELibro);
            return View(autorELibro);
        }

        // GET: AutorELibro/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AutorELibros == null)
            {
                return NotFound();
            }

            var autorELibro = await _context.AutorELibros
                .Include(a => a.IdAutorNavigation)
                .Include(a => a.IdELibroNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (autorELibro == null)
            {
                return NotFound();
            }

            return View(autorELibro);
        }

        // POST: AutorELibro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var autorELibro = await _context.AutorELibros.FindAsync(id);
            if (autorELibro != null)
            {
                _context.AutorELibros.Remove(autorELibro);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AutorELibroExists(int id)
        {
            return (_context.AutorELibros?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
