using Microsoft.AspNetCore.Mvc;

namespace Loboteca1.Controllers
{
    public class BusquedaController : Controller
    {
        // Acción para mostrar la vista de búsqueda
        [HttpGet]
        public IActionResult Busqueda()
        {
            return View();
        }

        // Acción para procesar la búsqueda (puedes agregar lógica aquí)
        [HttpPost]
        public IActionResult BuscarLibros(string titulo, string autor, string isbn, string editorial, string genero)
        {
            // Lógica para procesar los filtros de búsqueda
            // Por ahora, redirige a la misma vista
            return RedirectToAction("Busqueda");
        }

        // Acción para mostrar todos los libros
        [HttpPost]
        public IActionResult MostrarTodos()
        {
            // Lógica para mostrar todos los libros
            return RedirectToAction("Busqueda");
        }

        // Acción para guardar los filtros de búsqueda preferidos
        [HttpPost]
        public IActionResult GuardarFiltro(string filtro)
        {
            // Lógica para guardar el filtro
            return RedirectToAction("Busqueda");
        }
    }
}
