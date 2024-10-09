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
    public class ConfiguracionPenalizacionesController : Controller
    {
        private readonly LobotecaContext _context;

        public ConfiguracionPenalizacionesController(LobotecaContext context)
        {
            _context = context;
        }

        // GET: ConfiguracionPenalizaciones
        public async Task<IActionResult> Index()
        {
              return _context.ConfiguracionPenalizaciones != null ? 
                          View(await _context.ConfiguracionPenalizaciones.ToListAsync()) :
                          Problem("Entity set 'LobotecaContext.ConfiguracionPenalizaciones'  is null.");
        }

        // GET: ConfiguracionPenalizaciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ConfiguracionPenalizaciones == null)
            {
                return NotFound();
            }

            var configuracionPenalizacione = await _context.ConfiguracionPenalizaciones
                .FirstOrDefaultAsync(m => m.IdPenalizacion == id);
            if (configuracionPenalizacione == null)
            {
                return NotFound();
            }

            return View(configuracionPenalizacione);
        }

        // GET: ConfiguracionPenalizaciones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ConfiguracionPenalizaciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPenalizacion,Nombre,Monto")] ConfiguracionPenalizacione configuracionPenalizacione)
        {
            if (ModelState.IsValid)
            {
                _context.Add(configuracionPenalizacione);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(configuracionPenalizacione);
        }

        // GET: ConfiguracionPenalizaciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ConfiguracionPenalizaciones == null)
            {
                return NotFound();
            }

            var configuracionPenalizacione = await _context.ConfiguracionPenalizaciones.FindAsync(id);
            if (configuracionPenalizacione == null)
            {
                return NotFound();
            }
            return View(configuracionPenalizacione);
        }

        // POST: ConfiguracionPenalizaciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPenalizacion,Nombre,Monto")] ConfiguracionPenalizacione configuracionPenalizacione)
        {
            if (id != configuracionPenalizacione.IdPenalizacion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(configuracionPenalizacione);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConfiguracionPenalizacioneExists(configuracionPenalizacione.IdPenalizacion))
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
            return View(configuracionPenalizacione);
        }

        // GET: ConfiguracionPenalizaciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ConfiguracionPenalizaciones == null)
            {
                return NotFound();
            }

            var configuracionPenalizacione = await _context.ConfiguracionPenalizaciones
                .FirstOrDefaultAsync(m => m.IdPenalizacion == id);
            if (configuracionPenalizacione == null)
            {
                return NotFound();
            }

            return View(configuracionPenalizacione);
        }

        // POST: ConfiguracionPenalizaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ConfiguracionPenalizaciones == null)
            {
                return Problem("Entity set 'LobotecaContext.ConfiguracionPenalizaciones'  is null.");
            }
            var configuracionPenalizacione = await _context.ConfiguracionPenalizaciones.FindAsync(id);
            if (configuracionPenalizacione != null)
            {
                _context.ConfiguracionPenalizaciones.Remove(configuracionPenalizacione);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConfiguracionPenalizacioneExists(int id)
        {
          return (_context.ConfiguracionPenalizaciones?.Any(e => e.IdPenalizacion == id)).GetValueOrDefault();
        }
    }
}
