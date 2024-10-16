﻿using Microsoft.AspNetCore.Mvc;
using Loboteca_v2.Models;
using System.Linq;
using Loboteva_v2.Models;

namespace Loboteca1.Controllers
{
    public class BiotecnologiaController : Controller
    {
        private readonly LobotecaContext _context;

        public BiotecnologiaController(LobotecaContext context)
        {
            _context = context;
        }

        public IActionResult Biotecnologia()
        {
            // Filtramos los 6 libros más recientes que pertenecen a la carrera de Biotecnología (suponiendo IdCarrera = 5)
            var librosRecientes = _context.ELibros
                .Where(l => l.IdCarrera == 1)
                .OrderByDescending(l => l.Id)
                .Take(6)
                .ToList();

            // Filtramos las 6 revistas más recientes que pertenecen a la carrera de Biotecnología
            var revistasRecientes = _context.Revista
                .Where(r => r.IdCarrera == 1)
                .OrderByDescending(r => r.Id)
                .Take(6)
                .ToList();

            // Pasamos los datos a la vista
            ViewBag.LibrosRecientes = librosRecientes;
            ViewBag.RevistasRecientes = revistasRecientes;

            return View();
        }
    }
}
