using System;
using System.Collections.Generic;

namespace Loboteva_v2.Models
{
    public partial class ELibro
    {
        public ELibro()
        {
            AutorELibros = new HashSet<AutorELibro>();
        }

        public int Id { get; set; }
        public string? Titulo { get; set; }
        public string? Isbn { get; set; }
        public DateTime? FechaDePublicacion { get; set; }
        public string? Genero { get; set; }
        public string? Estado { get; set; }
        public string? RutaImagen { get; set; }
        public string? Archivo { get; set; }
        public DateTime? FechaDeAlta { get; set; }
        public int? IdEditorial { get; set; }
        public int? IdCarrera { get; set; }  // Nueva columna para la relación con Carrera

        public virtual Editorial? IdEditorialNavigation { get; set; }
        public virtual Carrera? IdCarreraNavigation { get; set; }  // Navegación a Carrera
        public virtual ICollection<AutorELibro> AutorELibros { get; set; }
    }
}
