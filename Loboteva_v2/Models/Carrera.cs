using System;
using System.Collections.Generic;

namespace Loboteva_v2.Models
{
    public partial class Carrera
    {
        public Carrera()
        {
            Alumnos = new HashSet<Alumno>();
            ELibros = new HashSet<ELibro>();  // Navegación a ELibro
        }

        public int Id { get; set; }
        public string NombreDeLaCarrera { get; set; } = null!;
        public string? Estado { get; set; }

        public virtual ICollection<Alumno> Alumnos { get; set; }
        public virtual ICollection<ELibro> ELibros { get; set; }  // Relación con ELibro
    }
}
