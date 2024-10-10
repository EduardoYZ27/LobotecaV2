using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Loboteva_v2.Models;

namespace Loboteva_v2.Controllers
{
    public class AlumnoController : Controller
    {
        private readonly LobotecaContext _context;

        public AlumnoController(LobotecaContext context)
        {
            _context = context;
        }

        // GET: Alumno
        public async Task<IActionResult> Index()
        {
            var lobotecaContext = _context.Alumnos.Include(a => a.oCarrera);
            return View(await lobotecaContext.ToListAsync());
        }

        // GET: Alumno/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Alumnos == null)
            {
                return NotFound();
            }

            var alumno = await _context.Alumnos
                .Include(a => a.oCarrera)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alumno == null)
            {
                return NotFound();
            }

            return View(alumno);
        }

        // GET: Alumno/Create
        public IActionResult Create()
        {
            ViewData["IdCarrera"] = new SelectList(_context.Carreras, "Id", "NombreDeLaCarrera");
            return View();
        }

        // POST: Alumno/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,ApellidoPaterno,ApellidoMaterno,Matricula,IdCarrera,Estado")] Alumno alumno)
        {
            if (ModelState.IsValid)
            {
                _context.Add(alumno);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCarrera"] = new SelectList(_context.Carreras, "Id", "NombreDeLaCarrera", alumno.IdCarrera);
            return View(alumno);
        }

        // GET: Alumno/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Alumnos == null)
            {
                return NotFound();
            }

            var alumno = await _context.Alumnos.FindAsync(id);
            if (alumno == null)
            {
                return NotFound();
            }
            ViewData["IdCarrera"] = new SelectList(_context.Carreras, "Id", "NombreDeLaCarrera", alumno.IdCarrera);
            return View(alumno);
        }

        // POST: Alumno/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,ApellidoPaterno,ApellidoMaterno,Matricula,IdCarrera,Estado")] Alumno alumno)
        {
            if (id != alumno.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(alumno);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlumnoExists(alumno.Id))
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
            ViewData["IdCarrera"] = new SelectList(_context.Carreras, "Id", "NombreDeLaCarrera", alumno.IdCarrera);
            return View(alumno);
        }

        // GET: Alumno/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Alumnos == null)
            {
                return NotFound();
            }

            var alumno = await _context.Alumnos
                .Include(a => a.oCarrera)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alumno == null)
            {
                return NotFound();
            }

            return View(alumno);
        }

        // POST: Alumno/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Alumnos == null)
            {
                return Problem("Entity set 'LobotecaContext.Alumnos'  is null.");
            }
            var alumno = await _context.Alumnos.FindAsync(id);
            if (alumno != null)
            {
                _context.Alumnos.Remove(alumno);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlumnoExists(int id)
        {
          return (_context.Alumnos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
