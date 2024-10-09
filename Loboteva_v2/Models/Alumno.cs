using System;
using System.Collections.Generic;

namespace Loboteva_v2.Models
{
    public partial class Alumno
    {
        public Alumno()
        {
            Sanciones = new HashSet<Sancione>();
        }

        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public string? Matricula { get; set; }
        public int? IdCarrera { get; set; }
        public string? Estado { get; set; }

        public virtual Carrera? oCarrera { get; set; }
        public virtual ICollection<Sancione> Sanciones { get; set; }
    }
}
