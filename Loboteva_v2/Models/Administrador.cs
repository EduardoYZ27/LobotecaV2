using System;
using System.Collections.Generic;

namespace Loboteva_v2.Models
{
    public partial class Administrador
    {
        public Administrador()
        {
            Prestamos = new HashSet<Prestamo>();
        }

        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public string? NumeroDeEmpleado { get; set; }
        public DateTime? FechaDeInicio { get; set; }
        public DateTime? FechaDeTermino { get; set; }

        public virtual ICollection<Prestamo> Prestamos { get; set; }
    }
}
